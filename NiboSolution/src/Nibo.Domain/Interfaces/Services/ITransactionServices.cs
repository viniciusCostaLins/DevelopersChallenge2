using Nibo.Domain.Entities.Transactions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nibo.Domain.Interfaces.Services
{
    public interface ITransactionServices : IDisposable
    {
        Task<bool> Add(IEnumerable<Transaction> transactions);
        Task<bool> Update(Transaction transaction);
        Task<bool> Delete(Guid id);
        IEnumerable<Transaction> ReadOfxFile(string filePath);
    }
}
