using System.Collections.Generic;

namespace Anonymized.Assessment.Services.Models.Responses
{
    public class Customer
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string Surname { get; set; }

        public double Balance { get; set; }

        public ICollection<Account> Accounts { get; set; }
    }
}