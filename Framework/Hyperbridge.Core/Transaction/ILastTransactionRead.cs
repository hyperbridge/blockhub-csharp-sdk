using Hyperbridge.Wallet;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Hyperbridge.Transaction
{
    public interface ILastTransactionRead<T> where T : ITransaction
    {
        Task<IEnumerable<T>> GetLastTransactions(string address, int startPage = 1, int limit = 100, CancellationToken cancelToken = default(CancellationToken));
    }
}
