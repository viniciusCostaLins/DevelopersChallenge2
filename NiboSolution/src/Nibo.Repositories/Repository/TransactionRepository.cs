using DevIO.Data.Repository;
using Nibo.Domain.Entities.Transactions;
using Nibo.Domain.Interfaces.Repositories;
using Nibo.Repositories.Context;

namespace Nibo.Repositories.Repository
{
    public class TransactionRepository : Repository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(NiboContext db) : base(db)
        {
        }
    }
}
