using Blockhub.Wallet;
using NBitcoin.BouncyCastle.Math;
using System;

namespace Blockhub.Ethereum
{
    public class Wei : ICurrency<Ethereum>
    {
        private readonly BigInteger _InternalAmount;
        public Wei(string amount)
        {
            _InternalAmount = new BigInteger(amount);
        }

        public Wei(long amount)
        {
            _InternalAmount = new BigInteger(amount.ToString());
        }

        public Wei(BigInteger amount)
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
            return _InternalAmount;
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
        public static Wei operator +(Wei left, Wei right)
        {
            return new Wei(left._InternalAmount.Add(right._InternalAmount));
        }

        public static Wei operator -(Wei left, Wei right)
        {
            return new Wei(left._InternalAmount.Subtract(right._InternalAmount));
        }
        #endregion
    }
}
