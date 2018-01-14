using Blockhub.Wallet;
using Newtonsoft.Json;
using System;

namespace Blockhub.Services
{
    public abstract class Transaction
    {
        public virtual DateTime TimeStamp { get; } = DateTime.UtcNow;
        public virtual string FromAddress { get; }
        public virtual string ToAddress { get; }

        public abstract IToken Amount { get; }
    }

    public class Transaction<T> : Transaction
        where T : ITokenSource
    {
        public Transaction(DateTime timestamp, IAccount<T> account, string toAddress, IToken<T> amount) : base()
        {
            TimeStamp = DateTime.UtcNow;
            Account = account ?? throw new ArgumentNullException(nameof(account));
            ToAddress = toAddress ?? throw new ArgumentNullException(nameof(toAddress));
            TokenAmount = amount ?? throw new ArgumentNullException(nameof(amount));
        }

        public override DateTime TimeStamp { get; }
        public override string FromAddress => Account.Address;
        public override string ToAddress { get; }

        public override IToken Amount => TokenAmount;
        public IToken<T> TokenAmount { get; }
        public IAccount<T> Account { get; }
    }
}
