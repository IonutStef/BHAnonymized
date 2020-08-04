using Anonymized.Assessment.Data.Contracts;

namespace Anonymized.Assessment.Data.Dtos
{
    public class CustomerDto : IDbEntity
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string Surname { get; set; }
    }
}