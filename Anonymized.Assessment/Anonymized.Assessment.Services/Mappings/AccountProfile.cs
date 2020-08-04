using AutoMapper;
using Anonymized.Assessment.Data.Dtos;
using Anonymized.Assessment.Services.Models.Requests;
using Anonymized.Assessment.Services.Models.Responses;
using System.Collections.Generic;

namespace Anonymized.Assessment.Services.Mappings
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<CreateAccountRequest, AccountDto>()
                .ReverseMap();

            CreateMap<AccountDto, Account>()
                .AfterMap((src, dest) => dest.Transactions = dest.Transactions ?? new List<Transaction>())
                .ReverseMap();
        }
    }
}