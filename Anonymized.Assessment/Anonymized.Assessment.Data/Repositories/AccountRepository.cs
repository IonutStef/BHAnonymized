using Anonymized.Assessment.Data.Dtos;
using Anonymized.Assessment.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Anonymized.Assessment.Data.Repositories
{
    public class AccountRepository : EntityFrameworkRepository<AccountDto>, IAccountRepository
    {
        public AccountRepository(AccountManagementContext context) : base(context)
        {
        }

        public async Task<ICollection<AccountDto>> GetAllAccountsForCustomerIdAsync(string customerId) =>
            await DbSet.Where(t => t.CustomerId == customerId).ToListAsync();
    }
}