using Blockhub.Transaction;
using Blockhub.Wallet;
using System.Threading.Tasks;

namespace Blockhub.Tests
{
    class EmulateTransactionWrite<T> : ITransactionWrite<T>
        where T : IBlockchainType
    {
        public EmulateTransactionWrite(TransactionSentResponse<T> response)
        {
            Response = response;
        }

        public TransactionSentResponse<T> Response { get; }

        public Task<TransactionSentResponse<T>> SendTransactionAsync(Account<T> fromAccount, string toAddress, ICurrency<T> amount)
        {
            return Task.FromResult(Response);
        }
    }
}
