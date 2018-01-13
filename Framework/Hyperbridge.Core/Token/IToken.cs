using System;
using System.Numerics;

namespace Hyperbridge.Wallet
{
    public interface IToken
    {
        string ToDisplayAmount();
    }

    public interface IToken<out T> : IToken
        where T : ITokenSource
    {
        decimal Amount { get; }
        string Unit { get; }
        string Name { get; }

        T TokenType { get; }

        BigInteger ToTransactionAmount();
    }
}