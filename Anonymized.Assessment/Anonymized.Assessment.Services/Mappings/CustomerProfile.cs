using AutoMapper;
using Anonymized.Assessment.Data.Dtos;
using Anonymized.Assessment.Services.Models.Requests;
using Anonymized.Assessment.Services.Models.Responses;
using System.Collections.Generic;

namespace Anonymized.Assessment.Services.Mappings
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<CreateCustomerRequest, CustomerDto>()
                .ReverseMap();

            CreateMap<CustomerDto, Customer>()
                .AfterMap((src, dest) => dest.Accounts = dest.Accounts ?? new List<Account>())
                .ReverseMap();
        }
    }
}