using Anonymized.Assessment.Api.Models.Requests;
using Anonymized.Assessment.Api.Models.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using Xunit.Fixture.Mvc.Extensions;

namespace Anonymized.Assessment.Api.IntegrationTests
{
    public class AccountManagementTests : TestBase
    {
        private const string AccountManagementPath = "AccountManagement";

        public AccountManagementTests(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public Task When_attempting_to_open_account_with_invalid_credit() =>
            GivenMvcFixture()
                .HavingModel<CreateAccountRequestModel>(out var request,
                    (_, c) =>
                    {
                        c.InitialCredit = -1;
                        c.CustomerId = "1";
                    })
                .WhenCreating(AccountManagementPath, request)
                .ShouldReturnBadRequest()
                .RunAsync();

        [Fact]
        public Task When_attempting_to_open_account_with_null_customerId() =>
            GivenMvcFixture()
                .HavingModel<CreateAccountRequestModel>(out var request,
                    (_, c) =>
                    {
                        c.InitialCredit = 1;
                        c.CustomerId = null;
                    })
                .WhenCreating(AccountManagementPath, request)
                .ShouldReturnBadRequest()
                .RunAsync();

        [Fact]
        public Task When_attempting_to_open_account_with_invalid_customerId() =>
            GivenMvcFixture()
                .HavingModel<CreateAccountRequestModel>(out var request,
                    (_, c) =>
                    {
                        c.InitialCredit = 1;
                        c.CustomerId = int.MaxValue.ToString();
                    })
                .WhenCreating(AccountManagementPath, request)
                .ShouldReturnBadRequest()
                .RunAsync();

        [Fact]
        public Task When_oppening_account_with_0_credit() =>
            GivenMvcFixture()
                .HavingModel<AccountModel>(out var expectedAccount, (_, a) =>
                {
                    a.CustomerId = "1";
                    a.Transactions = new List<TransactionModel>();
                })
                .HavingModel<CreateAccountRequestModel>(out var request, 
                    (_, c) => 
                        {
                            c.InitialCredit = 0;
                            c.CustomerId = "1";
                        })
                .WhenCreating(AccountManagementPath, request)
                .ShouldReturnSuccessfulStatus()
                .ShouldReturnEquivalentJson(expectedAccount, o => o.Excluding(a => a.Id))
                .RunAsync();

        [Fact]
        public Task When_oppening_account_with_credit() =>
            GivenMvcFixture()
                .HavingModel<AccountModel>(out var expectedAccount, (_, a) =>
                {
                    a.CustomerId = "1";
                    a.Transactions = new List<TransactionModel> {
                        new TransactionModel
                        {
                            Amount = 20
                        }
                    };
                })
                .HavingModel<CreateAccountRequestModel>(out var request,
                    (_, c) =>
                    {
                        c.InitialCredit = 20;
                        c.CustomerId = "1";
                    })
                .WhenCreating(AccountManagementPath, request)
                .ShouldReturnSuccessfulStatus()
                .ShouldReturnEquivalentJson(expectedAccount, o => o
                                .Excluding(a => a.Id)
                                .Excluding(a => ((List<TransactionModel>)a.Transactions)[0].AccountId)
                                .Excluding(a => ((List<TransactionModel>)a.Transactions)[0].Id))
                .RunAsync();
    }
}