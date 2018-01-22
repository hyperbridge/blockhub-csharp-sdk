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

        public abstract ICurrency Amount { get; }
    }

    public class Transaction<T> : Transaction
        where T : IBlockchainType
    {
        public Transaction(DateTime timestamp, Account<T> account, string toAddress, ICurrency<T> amount) : base()
        {
            TimeStamp = DateTime.UtcNow;
            Account = account ?? throw new ArgumentNullException(nameof(account));
            ToAddress = toAddress ?? throw new ArgumentNullException(nameof(toAddress));
            TokenAmount = amount ?? throw new ArgumentNullException(nameof(amount));
        }

        public override DateTime TimeStamp { get; }
        public override string FromAddress => Account.Address;
        public override string ToAddress { get; }

        public override ICurrency Amount => TokenAmount;
        public ICurrency<T> TokenAmount { get; }
        public Account<T> Account { get; }
    }
}
