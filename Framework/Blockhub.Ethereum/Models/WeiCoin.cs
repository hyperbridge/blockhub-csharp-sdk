using Blockhub.Wallet;
using System;
using System.Numerics;

namespace Blockhub.Ethereum
{
    public class WeiCoin : IToken<Ethereum>
    {
        private readonly BigInteger _InternalAmount;
        public WeiCoin(BigInteger amount)
        {
            _InternalAmount = amount;
        }

        // HACK: Need a way to safely convert from a BigInteger if it goes outside the bounds of decimal
        public decimal Amount => (decimal) _InternalAmount;
        public string Unit => "WEI";
        public string Name => "Wei";

        public Ethereum TokenType => Ethereum.Instance;

        public BigInteger ToTransactionAmount()
        {
            return new BigInteger(Amount);
        }

        public string ToDisplayAmount()
        {
            return ToString();
        }

        public override string ToString()
        {
            return $"{Amount} {Unit}";
        }

        public static WeiCoin operator +(WeiCoin left, WeiCoin right)
        {
            return new WeiCoin(left._InternalAmount + right._InternalAmount);
        }

        public static WeiCoin operator -(WeiCoin left, WeiCoin right)
        {
            return new WeiCoin(left._InternalAmount - right._InternalAmount);
        }
    }
}
