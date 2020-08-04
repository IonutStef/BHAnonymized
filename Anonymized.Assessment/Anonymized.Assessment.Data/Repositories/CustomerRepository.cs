using Anonymized.Assessment.Data.Dtos;
using Anonymized.Assessment.Data.Repositories.Interfaces;

namespace Anonymized.Assessment.Data.Repositories
{
    public class CustomerRepository : EntityFrameworkRepository<CustomerDto>, ICustomerRepository
    {
        public CustomerRepository(AccountManagementContext context) : base(context)
        {
        }
    }
}