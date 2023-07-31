﻿
using System;
using System.Collections;
using System.Collections.Generic;

namespace HomeBankingMinHub.Models
{
    public class AccountDTO
    {
        public long Id { get; set; }

        public string Number { get; set; }

        public DateTime CreationDate { get; set; }

        public double Balance { get; set; }

        public ICollection<TransactionDTO> Transactions { get; set; }
    }
}
