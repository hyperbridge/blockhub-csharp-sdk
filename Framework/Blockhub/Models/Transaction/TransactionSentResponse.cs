using Blockhub.Services;
using Blockhub.Wallet;
using System;

namespace Blockhub.Transaction
{
    public class TransactionSentResponse<T> where T : IBlockchainType
    {
        public TransactionSentResponse(Account<T> from, string to, ICurrency<T> amount, string transHash)
        {
            FromAccount = from ?? throw new ArgumentNullException(nameof(from));
            ToAccount = to ?? throw new ArgumentNullException(nameof(to));
            Amount = amount ?? throw new ArgumentNullException(nameof(amount));
            TransactionHash = transHash ?? throw new ArgumentNullException(nameof(transHash));
        }

        public Account<T> FromAccount { get; }
        public string ToAccount { get; }
        public ICurrency<T> Amount { get; }

        public string TransactionHash { get; }
    }
}
