using AutoMapper;
using Anonymized.Assessment.Data.Dtos;
using Anonymized.Assessment.Services.Models.Requests;
using Anonymized.Assessment.Services.Models.Responses;

namespace Anonymized.Assessment.Services.Mappings
{
    public class TransactionProfile : Profile
    {
        public TransactionProfile()
        {
            CreateMap<CreateTransactionRequest, TransactionDto>()
                .ReverseMap();

            CreateMap<TransactionDto, Transaction>()
                .ReverseMap();
        }
    }
}