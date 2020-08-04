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
    public class AccountService : IAccountService
    {
        private readonly ILogger<AccountService> _logger;
        private readonly IAccountRepository _accountRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public AccountService(ILogger<AccountService> logger, IAccountRepository accountRepository, 
            ICustomerRepository customerRepository, IMapper mapper)
        {
            _logger = logger;
            _accountRepository = accountRepository;
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<Account> OpenAccountAsync(CreateAccountRequest request)
        {
            if(request == null)
            {
                throw BadRequestException.Raise<CreateAccountRequest>();
            }

            _logger.LogInformation($"Oppening an account for customer: {request.CustomerId}.");

            var customer = await _customerRepository.GetAsync(request.CustomerId);

            if (customer == null)
            {
                _logger.LogInformation($"No customer found for id: {request.CustomerId}.");
                throw BadRequestException.Raise<CreateAccountRequest>(r => r.CustomerId, request.CustomerId);
            }

            var accountDto = new AccountDto
            {
                CustomerId = request.CustomerId
            };

            accountDto = await _accountRepository.AddAsync(accountDto);

            _logger.LogInformation($"Account: {accountDto.Id} opened for customer: {accountDto.CustomerId}.");

            var account = _mapper.Map<Account>(accountDto);

            return account;
        }
    }
}