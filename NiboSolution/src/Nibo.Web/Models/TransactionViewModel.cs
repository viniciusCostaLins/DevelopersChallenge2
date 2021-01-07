using System;

namespace Nibo.Web.Models
{
    public class TransactionViewModel
    {
        public decimal Amount { get; set; }
        public string Memo { get; set; }
        public DateTime DatePosted { get; set; }
        public string TransactionType { get; set; }
        public AccountViewModel Account { get; set; }
        public Guid AccountId { get; set; }
    }
}
