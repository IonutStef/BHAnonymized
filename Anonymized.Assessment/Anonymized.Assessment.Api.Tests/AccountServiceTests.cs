using AutoMapper;
using Anonymized.Assessment.Data.Dtos;
using Anonymized.Assessment.Data.Repositories.Interfaces;
using Anonymized.Assessment.Services.Exceptions;
using Anonymized.Assessment.Services.Models.Requests;
using Anonymized.Assessment.Services.Models.Responses;
using GivenFixture;
using GivenFixture.Extensions;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace Anonymized.Assessment.Services.UnitTests
{
    public class AccountServiceTests
    {
        /// <summary>
        /// GivenFixture is a wrapper around TestFixture,
        /// used to test methods by testing the output and the dependencies inside the method.
        /// https://ax-h.com/software/development/testing/2018/12/09/fluent-unit-testing.html
        /// </summary>
        [Fact(Skip = "Test will fail because a dependency was mocked, but never called.")]
        public Task When_attempting_to_open_account_with_null_request_and_mocked_dependency() =>
            Given.Fixture
                // The ICustomerRepository.GetAsync call was mocked, but the OpenAccountAsync method will return before reaching it. 
                // This is the expected behavior, because of the null parameter.
                .HavingMockedAsync<ICustomerRepository, CustomerDto>(i => i.GetAsync(It.IsAny<string>()), null) 
                .When<AccountService, Account>(a => a.OpenAccountAsync(null))
                .ShouldThrow<BadRequestException>()
                .RunAsync();

        [Fact]
        public Task When_attempting_to_open_account_with_null_request() =>
            Given.Fixture
                .When<AccountService, Account>(a => a.OpenAccountAsync(null))
                .ShouldThrow<BadRequestException>()
                .RunAsync();

        [Fact]
        public Task When_attempting_to_open_account_with_invalid_customerId() =>
            Given.Fixture
                .HavingModel<CreateAccountRequest>(out var request)
                .HavingMockedAsync<ICustomerRepository, CustomerDto>(i => i.GetAsync(It.IsAny<string>()), null)
                .When<AccountService, Account>(a => a.OpenAccountAsync(request))
                .ShouldThrow<BadRequestException>()
                .RunAsync();

        [Fact]
        public Task When_opening_account() =>
            Given.Fixture
                .HavingModel<CreateAccountRequest>(out var request)
                .HavingModel<CustomerDto>(out var customerDto)
                .HavingModel<Account>(out var account)
                .HavingModel<AccountDto>(out var accountDto)
                .HavingMockedAsync<ICustomerRepository, CustomerDto>(i => i.GetAsync(It.IsAny<string>()), out _)
                .HavingMockedAsync<IAccountRepository, AccountDto>(i => i.AddAsync(It.IsAny<AccountDto>()), accountDto)
                .HavingMocked<IMapper, Account>(m => m.Map<Account>(accountDto), account)
                .When<AccountService, Account>(a => a.OpenAccountAsync(request))
                .ShouldReturnEquivalent(account)
                .RunAsync();
    }
}