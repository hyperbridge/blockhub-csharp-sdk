using Hyperbridge.Wallet;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hyperbridge.Services.Abstract
{
    public interface ITransactionRead
    {
        Task<IEnumerable<Transaction>> GetLastTransactions(Account account, int page = 1, int limit = 25);
        Task<IEnumerable<SendTransaction>> GetLastSentTransactions(Account account, int page = 1, int limit = 25);
        Task<IEnumerable<ReceiveTransaction>> GetLastReceivedTransactions(Account account, int page = 1, int limit = 25);
    }
}
