using System;
using System.Numerics;

namespace Hyperbridge.Wallet
{
    public interface IToken<out T> where T : ITokenSource
    {
        decimal Amount { get; }
        string Unit { get; }
        string Name { get; }

        T BaseCurrency { get; }

        BigInteger ToTransactionAmount();
    }
}