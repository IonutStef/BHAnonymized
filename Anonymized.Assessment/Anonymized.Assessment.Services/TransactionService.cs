using AutoMapper;
using Anonymized.Assessment.Data.Dtos;
using Anonymized.Assessment.Data.Repositories.Interfaces;
using Anonymized.Assessment.Services.Exceptions;
using Anonymized.Assessment.Services.Interfaces;
using Anonymized.Assessment.Services.Models.Requests;
using Anonymized.Assessment.Services.Models.Responses;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Anonymized.Assessment.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ILogger<TransactionService> _logger;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;

        public TransactionService(ILogger<TransactionService> logger, ITransactionRepository transactionRepository, 
            IAccountRepository accountRepository, IMapper mapper)
        {
            _logger = logger;
            _transactionRepository = transactionRepository;
            _accountRepository = accountRepository;
            _mapper = mapper;
        }

        public async Task<Transaction> InitiateTransactionAsync(CreateTransactionRequest request)
        {
            if (request == null)
            {
                throw BadRequestException.Raise<CreateTransactionRequest>();
            }

            _logger.LogInformation($"Initiating a transaction for account: {request.AccountId}, with {request.Amount} amount.");

            if (request.Amount == 0)
            {
                return null;
            }

            if (request.Amount < 0)
            {
                _logger.LogInformation($"Invalid amount: {request.Amount}, when initiating " +
                    $"a transaction for account: {request.AccountId}.");

                throw BadRequestException.Raise<Transaction>(t => t.Amount, request.Amount);
            }

            if (!await _accountRepository.ExistsAsync(request.AccountId))
            {
                _logger.LogInformation($"Account not found for id: {request.AccountId}.");

                throw BadRequestException.Raise<CreateTransactionRequest>(r => r.AccountId, request.AccountId);
            }

            if (request.Amount > 0)
            {
                var transactionDto = new TransactionDto
                {
                    AccountId = request.AccountId,
                    Amount = request.Amount
                };
                transactionDto = await _transactionRepository.AddAsync(transactionDto);

                _logger.LogInformation($"Transaction: {transactionDto.Id} " +
                    $"initiated for account: {transactionDto.AccountId}, with {transactionDto.Amount} amount.");

                return _mapper.Map<Transaction>(transactionDto);
            }

            return null;
        }
    }
}