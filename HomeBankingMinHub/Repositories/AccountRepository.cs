using HomeBankingMinHub.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
namespace HomeBankingMinHub.Repositories
{
    public class AccountRepository : RepositoryBase<Account>, IAccountRepository

    {
        public AccountRepository(HomeBankingContext repositoryContext) : base(repositoryContext)
        {
        }

        public Account FindById(long id)
        {
            return FindByCondition(t => t.Id == id)
               .Include(t => t.Transactions)
               .FirstOrDefault();
        }

        public IEnumerable<Account> GetAllAccounts()
        {
            return FindAll()
               .Include(trans => trans.Transactions)
               .ToList();
        }

        public void Save(Account accounts)
        {
            Create(accounts);
            SaveChanges();
        }
    }
}
