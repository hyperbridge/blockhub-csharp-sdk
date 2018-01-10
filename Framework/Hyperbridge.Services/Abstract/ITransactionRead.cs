using Hyperbridge.Wallet;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hyperbridge.Services.Abstract
{
    public interface ITransactionRead
    {
        Task<IEnumerable<Transaction>> GetLastTransactions(int page = 1, int limit = 25);
        Task<IEnumerable<Transaction>> GetLastSentTransactions(int page = 1, int limit = 25);
        Task<IEnumerable<Transaction>> GetLastReceivedTransactions(int page = 1, int limit = 25);
    }

    public interface ITransactionWrite
    {
        Task SendTransaction(Transaction transaction);
    }
}
