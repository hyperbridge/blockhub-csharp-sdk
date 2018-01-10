using System;
using System.Numerics;

namespace Hyperbridge.Wallet
{
    public interface ICoin<out T> where T : ICoinCurrency
    {
        decimal Amount { get; }
        T BaseCurrency { get; }

        BigInteger ToTransactionAmount();
    }
}