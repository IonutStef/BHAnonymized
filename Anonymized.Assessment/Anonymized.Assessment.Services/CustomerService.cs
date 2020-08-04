using AutoMapper;
using Anonymized.Assessment.Data.Dtos;
using Anonymized.Assessment.Data.Repositories.Interfaces;
using Anonymized.Assessment.Services.Exceptions;
using Anonymized.Assessment.Services.Interfaces;
using Anonymized.Assessment.Services.Models.Requests;
using Anonymized.Assessment.Services.Models.Responses;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Anonymized.Assessment.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ILogger<CustomerService> _logger;
        private readonly ICustomerRepository _customerRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IMapper _mapper;

        public CustomerService(ILogger<CustomerService> logger, ICustomerRepository customerRepository, 
            IAccountRepository accountRepository, ITransactionRepository transactionRepository, IMapper mapper)
        {
            _logger = logger;
            _customerRepository = customerRepository;
            _accountRepository = accountRepository;
            _transactionRepository = transactionRepository;
            _mapper = mapper;
        }

        public async Task<Customer> GetCustomerAsync(string customerId)
        {
            _logger.LogInformation($"Getting customer for id: {customerId}.");

            var customerDto = await _customerRepository.GetAsync(customerId);

            if(customerDto == null)
            {
                _logger.LogInformation($"No customer found for id: {customerId}.");

                throw NotFoundException.Raise<Customer>(c => c.Id, customerId);
            }

            var customer = _mapper.Map<Customer>(customerDto);

            var accountsDtos = await _accountRepository.GetAllAccountsForCustomerIdAsync(customer.Id);

            if(accountsDtos == null || accountsDtos.Count == 0)
            {
                _logger.LogInformation($"No accounts found for customer: {customer.Id}.");

                return customer;
            }

            var accounts = _mapper.Map<ICollection<Account>>(accountsDtos);

            foreach(var account in accounts)
            {
                account.Transactions = await GetTransactionsForAccountId(account.Id);
            }

            customer.Accounts = accounts;
            customer.Balance = customer.Accounts.SelectMany(a => a.Transactions).Sum(t => t.Amount);

            return customer;
        }

        public ICollection<Customer> InsertCustomers(ICollection<CreateCustomerRequest> customers)
        {
            if(customers == null || customers.Count == 0)
            {
                _logger.LogInformation($"Invalid customers to be inserted.");

                throw BadRequestException.Raise<ICollection<CreateCustomerRequest>>();
            }

            var addedCustomers = _customerRepository.AddRange(_mapper.Map<ICollection<CustomerDto>>(customers));

            _logger.LogInformation($"{addedCustomers.Count} customers added to the database. " +
                $"\n\t{string.Join(", ", addedCustomers.Select(c => c.Id).ToList())}");

            return _mapper.Map<ICollection<Customer>>(addedCustomers);
        }

        private async Task<ICollection<Transaction>> GetTransactionsForAccountId(string accountId)
        {
            var transactions = await _transactionRepository.GetAllTransactionsForAccountIdAsync(accountId);

            if (transactions == null || transactions.Count == 0)
            {
                _logger.LogInformation($"Transactions not found for account: {accountId}.");

                return new List<Transaction>();
            }

            _logger.LogInformation($"{transactions.Count} found for account: {accountId}. " +
                $"\n\t {string.Join(", ", transactions.Select(t => t.Id).ToList())}");
            return _mapper.Map<ICollection<Transaction>>(transactions);
        }
    }
}