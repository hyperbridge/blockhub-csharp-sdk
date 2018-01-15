using Blockhub.Wallet;
using NBitcoin.BouncyCastle.Math;
using System;

namespace Blockhub.Ethereum
{
    public class WeiCoin : IToken<Ethereum>
    {
        private readonly BigInteger _InternalAmount;
        public WeiCoin(string amount)
        {
            _InternalAmount = new BigInteger(amount);
        }

        public WeiCoin(long amount)
        {
            _InternalAmount = new BigInteger(amount.ToString());
        }

        public WeiCoin(BigInteger amount)
        {
            _InternalAmount = amount;
        }

        // HACK: Need a way to safely convert from a BigInteger if it goes outside the bounds of decimal
        public decimal Amount => _InternalAmount.LongValue;
        public string Unit => "WEI";
        public string Name => "Wei";

        public Ethereum TokenType => Ethereum.Instance;

        public BigInteger ToTransactionAmount()
        {
            return new BigInteger(Amount.ToString());
        }

        public string ToDisplayAmount()
        {
            return ToString();
        }

        public override string ToString()
        {
            return $"{Amount} {Unit}";
        }

        #region Operators
        public static WeiCoin operator +(WeiCoin left, WeiCoin right)
        {
            return new WeiCoin(left._InternalAmount.Add(right._InternalAmount));
        }

        public static WeiCoin operator -(WeiCoin left, WeiCoin right)
        {
            return new WeiCoin(left._InternalAmount.Subtract(right._InternalAmount));
        }
        #endregion
    }
}
