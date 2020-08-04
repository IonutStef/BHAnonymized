using AutoMapper;
using Anonymized.Assessment.Api.Models.Requests;
using Anonymized.Assessment.Api.Models.Responses;
using Anonymized.Assessment.Services.Models.Requests;
using Anonymized.Assessment.Services.Models.Responses;
using System.Collections.Generic;

namespace Anonymized.Assessment.Api.Mappings
{
    /// <summary>
    /// Automapper profile for <see cref="AccountModel"/> related type objects.
    /// </summary>
    public class AccountProfile : Profile
    {
        /// <summary>
        /// Contructor to configure automapper for <see cref="AccountModel"/> related type objects.
        /// </summary>
        public AccountProfile()
        {
            CreateMap<CreateAccountRequestModel, CreateAccountRequest>()
                .ReverseMap();

            CreateMap<Account, AccountModel>()
                .AfterMap((src, dest) => dest.Transactions = dest.Transactions ?? new List<TransactionModel>())
                .ReverseMap();
        }
    }
}