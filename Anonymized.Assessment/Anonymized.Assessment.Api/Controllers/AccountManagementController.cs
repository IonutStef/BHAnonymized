using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using AutoMapper;
using Anonymized.Assessment.Api.Models.Requests;
using Anonymized.Assessment.Api.Models.Responses;
using Anonymized.Assessment.Services.Interfaces;
using Anonymized.Assessment.Services.Models.Requests;
using Anonymized.Assessment.Services.Models.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Anonymized.Assessment.Api.Controllers
{
    /// <summary>
    /// Controller for account management actions.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AccountManagementController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly ITransactionService _transactionService;
        private readonly IMapper _mapper;

        public AccountManagementController(IAccountService accountService, ITransactionService transactionService, IMapper mapper)
        {
            _accountService = accountService;
            _transactionService = transactionService;
            _mapper = mapper;
        }

        /// <summary>
        /// Open an account according to the <paramref name="request"/>.
        /// If amount from <paramref name="request"/> is greater than 0, a transaction will be initiated.
        /// </summary>
        /// <param name="request">The data after which the account should be oppened.</param>
        /// <response code="200">Returns opened account.</response>
        /// <response code="400">Invalid data.</response>
        /// <returns>The opened account.</returns>
        [HttpPost]
        public async Task<ActionResult<AccountModel>> OpenAccount([Required] CreateAccountRequestModel request)
        {
            var account = await _accountService.OpenAccountAsync(_mapper.Map<CreateAccountRequest>(request));
            account.Transactions = new List<Transaction>();

            var transaction = await _transactionService.InitiateTransactionAsync(
                    new CreateTransactionRequest
                    {
                        AccountId = account.Id,
                        Amount = request.InitialCredit
                    });
            if(transaction != null)
            {
                account.Transactions.Add(transaction);
            }

            return _mapper.Map<AccountModel>(account);
        }
    }
}