
using Nibo.Domain.Entities.Abstract;
using Nibo.Domain.Entities.Transactions;
using System.Collections.Generic;

namespace Nibo.Domain.Entities.Accounts
{
    public class Account : Entity
    {        
        public string BankId { get; set; }
        public string AccountId { get; set; }
        public string AccountType { get; set; }

        public IEnumerable<Transaction> Transactions { get; set; }
    }
}
