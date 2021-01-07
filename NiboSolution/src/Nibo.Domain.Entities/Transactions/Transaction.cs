using Nibo.Domain.Entities.Abstract;
using Nibo.Domain.Entities.Accounts;
using System;

namespace Nibo.Domain.Entities.Transactions
{
    public class Transaction : Entity
    {
        public decimal Amount { get; set; }
        public string Memo { get; set; }
        public DateTime DatePosted { get; set; }
        public TransactionType TransactionType { get; set; }
        public Account Account { get; set; }
        public Guid? AccountId { get; set; }
    }
}
