using System;
using System.Collections.Generic;

namespace HomeBankingMinHub.Models
{
    public class TransactionDTO
    {
        public long Id { get; set; }

        public string Type { get; set; }

        public double Amount { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }

        public ICollection<TransactionDTO> Transactions { get; set; }

    }
}
