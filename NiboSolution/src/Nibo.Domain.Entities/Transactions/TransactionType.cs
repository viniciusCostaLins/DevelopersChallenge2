
using System.ComponentModel;

namespace Nibo.Domain.Entities.Transactions
{
    public enum TransactionType
    {
        [Description("DEBIT")]
        Debit,
        [Description("CREDIT")]
        Credit
    }
}
