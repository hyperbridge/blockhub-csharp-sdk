using Blockhub.Wallet;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Blockhub.Transaction
{
    public interface ILastTransactionRead<T> where T : ITransaction
    {
        Task<IEnumerable<T>> GetLastTransactions(string address, int startPage = 1, int limit = 100, CancellationToken cancelToken = default(CancellationToken));
    }
}
