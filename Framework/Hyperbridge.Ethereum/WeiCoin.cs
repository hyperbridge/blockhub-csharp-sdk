using Hyperbridge.Wallet;
using System;
using System.Numerics;

namespace Hyperbridge.Ethereum
{
    public class WeiCoin : ICoin<Ether>
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

        public Ether BaseCurrency => Ether.Instance;

        

        public BigInteger ToTransactionAmount()
        {
            return new BigInteger(Amount);
        }

        public override string ToString()
        {
            return $"{Amount} WEI";
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
