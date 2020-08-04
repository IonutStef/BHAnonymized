using AutoMapper;
using Anonymized.Assessment.Api.Models.Responses;
using Anonymized.Assessment.Services.Models.Responses;

namespace Anonymized.Assessment.Api.Mappings
{
    /// <summary>
    /// Automapper profile for <see cref="TransactionModel"/> related type objects.
    /// </summary>
    public class TransactionProfile : Profile
    {
        /// <summary>
        /// Contructor to configure automapper for <see cref="TransactionModel"/> related type objects.
        /// </summary>
        public TransactionProfile()
        {
            CreateMap<Transaction, TransactionModel>()
                .ReverseMap();
        }
    }
}