using Hyperbridge.Wallet;
using System;

namespace Hyperbridge.Ethereum
{
    public class WeiCoin : ICoin<Ether>
    {
        public WeiCoin(decimal amount)
        {
            // HACK: Can we utilize something else (like an int) to ensure a whole number?
            if (System.Math.Round(amount) != amount) throw new ArgumentOutOfRangeException(nameof(amount), "Amount must be a whole number.");
            Amount = amount;
        }

        public decimal Amount { get; }
        public Ether BaseCurrency => Ether.Instance;

        public decimal ToTransactionAmount()
        {
            return Amount * 1E-18M;
        }

        public override string ToString()
        {
            return $"{Amount} WEI";
        }

        public static WeiCoin operator +(WeiCoin left, WeiCoin right)
        {
            return new WeiCoin(left.Amount + right.Amount);
        }

        public static WeiCoin operator -(WeiCoin left, WeiCoin right)
        {
            return new WeiCoin(left.Amount - right.Amount);
        }
    }
}
