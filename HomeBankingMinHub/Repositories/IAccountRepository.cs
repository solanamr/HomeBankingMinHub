﻿using HomeBankingMinHub.Models;
using System.Collections.Generic;

namespace HomeBankingMinHub.Repositories
{
    public interface IAccountRepository
    {
        IEnumerable<Account> GetAllAccounts();
        Account FindById(long id);
        void Save(Account account);
        IEnumerable<Account> GetAccountsByClient(long clientId);
        Account FindByNumber(string number);
    }
}
