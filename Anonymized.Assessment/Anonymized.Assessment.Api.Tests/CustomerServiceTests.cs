using AutoMapper;
using Anonymized.Assessment.Data.Dtos;
using Anonymized.Assessment.Data.Repositories.Interfaces;
using Anonymized.Assessment.Services.Exceptions;
using Anonymized.Assessment.Services.Models.Requests;
using Anonymized.Assessment.Services.Models.Responses;
using FluentAssertions;
using GivenFixture;
using GivenFixture.Extensions;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Anonymized.Assessment.Services.UnitTests
{
    public class CustomerServiceTests
    {
        [Fact]
        public void When_attempting_to_insert_customers_with_null_request() =>
            Given.Fixture
                .When<CustomerService, ICollection<Customer>>(c => c.InsertCustomers(null))
                .ShouldThrow<BadRequestException>(e => e.Type.Should().Be(typeof(ICollection<CreateCustomerRequest>)))
                .Run();

        [Fact]
        public void When_insert_customers() =>
            Given.Fixture
                .HavingModels<CreateCustomerRequest>(out var request)
                .HavingModels<CustomerDto>(out var customersDtos)
                .HavingModels<Customer>(out var customers)
                .HavingMocked<IMapper, ICollection<CustomerDto>>(m => m.Map<ICollection<CustomerDto>>(request), customersDtos)
                .HavingMocked<IMapper, ICollection<Customer>>(m => m.Map<ICollection<Customer>>(customersDtos), customers)
                .HavingMocked<ICustomerRepository, ICollection<CustomerDto>>(i => i.AddRange(customersDtos), customersDtos)
                .When<CustomerService, ICollection<Customer>>(c => c.InsertCustomers(request))
                .ShouldReturnEquivalent(customers)
                .Run();

        [Fact]
        public Task When_attempting_to_get_customer_with_invalid_id() =>
            Given.Fixture
                .HavingFake(f => f.Random.AlphaNumeric(5), out var customerId)
                .HavingMockedAsync<ICustomerRepository, CustomerDto>(i => i.GetAsync(customerId), null)
                .When<CustomerService, Customer>(c => c.GetCustomerAsync(customerId))
                .ShouldThrow<NotFoundException>(e =>
                {
                    e.Type.Should().Be(typeof(Customer));
                    e.Key.Should().Be(customerId);
                })
                .RunAsync();

        [Fact]
        public Task When_getting_customer_without_accounts() =>
            Given.Fixture
                .HavingFake(f => f.Random.AlphaNumeric(5), out var customerId)
                .HavingModel<Customer>(out var customer, c => c.With(e => e.Accounts, (ICollection<Account>)null).With(e => e.Balance, 0))
                .HavingMockedAsync<ICustomerRepository, CustomerDto>(i => i.GetAsync(customerId), out var customerDto)
                .HavingMocked<IMapper, Customer>(m => m.Map<Customer>(customerDto), customer)
                .HavingMockedAsync<IAccountRepository, ICollection<AccountDto>>(i => i.GetAllAccountsForCustomerIdAsync(customer.Id), null)
                .When<CustomerService, Customer>(c => c.GetCustomerAsync(customerId))
                .ShouldReturnEquivalent(customer)
                .RunAsync();

        [Fact]
        public Task When_getting_customer_with_accounts_and_without_transactions() =>
            Given.Fixture
                .HavingFake(f => f.Random.AlphaNumeric(5), out var customerId)
                .HavingModels<AccountDto>(out var accountsDtos)
                .HavingModels<Account>(out var accounts, c => c.With(a => a.Transactions, new List<Transaction>()))
                .HavingModel<Customer>(out var customer, c => c.With(e => e.Accounts, (ICollection<Account>)null).With(e => e.Balance, 0))
                .HavingMockedAsync<ICustomerRepository, CustomerDto>(i => i.GetAsync(customerId), out var customerDto)
                .HavingMocked<IMapper, Customer>(m => m.Map<Customer>(customerDto), customer)
                .HavingMockedAsync<IAccountRepository, ICollection<AccountDto>>(i => i.GetAllAccountsForCustomerIdAsync(customer.Id), accountsDtos)
                .HavingMocked<IMapper, ICollection<Account>>(m => m.Map<ICollection<Account>>(accountsDtos), accounts)
                .HavingMockedAsync<ITransactionRepository, ICollection<TransactionDto>>(i => i.GetAllTransactionsForAccountIdAsync(It.IsAny<string>()), null)
                .When<CustomerService, Customer>(c => c.GetCustomerAsync(customerId))
                .ShouldReturnEquivalent(customer)
                .ShouldReturn<Customer>(c =>
                {
                    c.Balance.Should().Be(0);
                    c.Accounts.Should().BeEquivalentTo(accounts);
                })
                .RunAsync();

        [Fact]
        public Task When_getting_customer_with_accounts_and_transactions() =>
            Given.Fixture
                .HavingFake(f => f.Random.AlphaNumeric(5), out var customerId)
                .HavingModel<AccountDto>(out var accountDto)
                .HavingModel<TransactionDto>(out var transactionDto)
                .HavingModel<Transaction>(out var transaction)
                .HavingModel<Account>(out var account)
                .HavingFake<ICollection<AccountDto>>(f => new List<AccountDto> { accountDto }, out var accountsDtos)
                .HavingFake<ICollection<Account>>(f => new List<Account> { account }, out var accounts)
                .HavingFake<ICollection<TransactionDto>>(f => new List<TransactionDto> { transactionDto }, out var transactionsDtos)
                .HavingFake<ICollection<Transaction>>(f => new List<Transaction> { transaction }, out var transactions)
                .HavingModel<Customer>(out var customer, c => c.With(e => e.Accounts, (ICollection<Account>)null).With(e => e.Balance, 0))
                .HavingMockedAsync<ICustomerRepository, CustomerDto>(i => i.GetAsync(customerId), out var customerDto)
                .HavingMocked<IMapper, Customer>(m => m.Map<Customer>(customerDto), customer)
                .HavingMockedAsync<IAccountRepository, ICollection<AccountDto>>(i => i.GetAllAccountsForCustomerIdAsync(customer.Id), accountsDtos)
                .HavingMocked<IMapper, ICollection<Account>>(m => m.Map<ICollection<Account>>(accountsDtos), accounts)
                .HavingMockedAsync<ITransactionRepository, ICollection<TransactionDto>>(i => i.GetAllTransactionsForAccountIdAsync(account.Id), transactionsDtos)
                .HavingMocked<IMapper, ICollection<Transaction>>(m => m.Map<ICollection<Transaction>>(transactionsDtos), transactions)
                .When<CustomerService, Customer>(c => c.GetCustomerAsync(customerId))
                .ShouldReturnEquivalent(customer)
                .ShouldReturn<Customer>(c =>
                {
                    c.Balance.Should().Be(transaction.Amount);
                    c.Accounts.Should().BeEquivalentTo(accounts);
                    account.Transactions.Should().BeEquivalentTo(transactions);
                })
                .RunAsync();
    }
}