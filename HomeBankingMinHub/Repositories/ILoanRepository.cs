using HomeBankingMinHub.Models;
using System.Collections;
using System.Collections.Generic;

namespace HomeBankingMinHub.Repositories
{
    public interface ILoanRepository
    {
        IEnumerable<Loan> GetAllLoans();
        Loan FindById(long id);
    }
}
