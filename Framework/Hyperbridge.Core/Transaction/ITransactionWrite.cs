using Blockhub.Wallet;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blockhub.Transaction
{
    public interface ITransactionWrite<T> where T : ITokenSource
    {
        Task<TransactionSentResponse<T>> SendTransactionAsync(IAccount<T> fromAccount, string toAddress, IToken<T> amount);
    }
}
