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
using System.Threading.Tasks;
using Xunit;

namespace Anonymized.Assessment.Services.UnitTests
{
    public class TransactionServiceTests
    {
        [Fact]
        public Task When_initiate_transaction_with_null_request() =>
            Given.Fixture
                .When<TransactionService, Transaction>(a => a.InitiateTransactionAsync(null))
                .ShouldThrow<BadRequestException>()
                .RunAsync();

        [Fact]
        public Task When_initiate_transaction_with_0_amount() =>
            Given.Fixture
                .HavingModel<CreateTransactionRequest>(out var request, c => c.With(t => t.Amount, 0))
                .When<TransactionService, Transaction>(a => a.InitiateTransactionAsync(request))
                .ShouldReturnNull()
                .RunAsync();

        [Fact]
        public Task When_initiate_transaction_with_invalid_accountId() =>
            Given.Fixture
                .HavingModel<CreateTransactionRequest>(out var request)
                .HavingMockedAsync<IAccountRepository, bool>(i => i.ExistsAsync(request.AccountId), false)
                .When<TransactionService, Transaction>(a => a.InitiateTransactionAsync(request))
                .ShouldThrow<BadRequestException>(e => e.Key.Should().Be(request.AccountId))
                .RunAsync();

        [Fact]
        public Task When_initiate_transaction() =>
            Given.Fixture
                .HavingModel<CreateTransactionRequest>(out var request)
                .HavingModel<TransactionDto>(out var transactionDto)
                .HavingModel<Transaction>(out var transaction)
                .HavingMockedAsync<IAccountRepository, bool>(i => i.ExistsAsync(request.AccountId), true)
                .HavingMockedAsync<ITransactionRepository, TransactionDto>(i => i.AddAsync(It.IsAny<TransactionDto>()), transactionDto)
                .HavingMocked<IMapper, Transaction>(i => i.Map<Transaction>(transactionDto), transaction)
                .When<TransactionService, Transaction>(a => a.InitiateTransactionAsync(request))
                .ShouldReturnEquivalent(transaction)
                .RunAsync();
    }
}