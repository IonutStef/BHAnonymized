using AutoMapper;
using Anonymized.Assessment.Api.Models.Responses;
using Anonymized.Assessment.Services.Models.Responses;
using System.Collections.Generic;

namespace Anonymized.Assessment.Api.Mappings
{
    /// <summary>
    /// Automapper profile for <see cref="CustomerModel"/> related type objects.
    /// </summary>
    public class CustomerProfile : Profile
    {
        /// <summary>
        /// Contructor to configure automapper for <see cref="CustomerModel"/> related type objects.
        /// </summary>
        public CustomerProfile()
        {
            CreateMap<Customer, CustomerModel>()
                .AfterMap((src, dest) => dest.Accounts = dest.Accounts ?? new List<AccountModel>())
                .ReverseMap();
        }
    }
}