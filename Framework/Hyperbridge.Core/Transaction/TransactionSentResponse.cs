using Hyperbridge.Wallet;
using System;

namespace Hyperbridge.Transaction
{
    public class TransactionSentResponse<T> where T : ICoinCurrency
    {
        public TransactionSentResponse(IAccount<T> from, IAccount<T> to, ICoin<T> amount, string transHash)
        {
            FromAccount = from ?? throw new ArgumentNullException(nameof(from));
            ToAccount = to ?? throw new ArgumentNullException(nameof(to));
            Amount = amount ?? throw new ArgumentNullException(nameof(amount));
            TransactionHash = transHash ?? throw new ArgumentNullException(nameof(transHash));
        }

        public IAccount<T> FromAccount { get; }
        public IAccount<T> ToAccount { get; }
        public ICoin<T> Amount { get; }

        public string TransactionHash { get; }
    }
}
