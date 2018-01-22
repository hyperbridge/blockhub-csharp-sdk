using NBitcoin.BouncyCastle.Math;
using System;

namespace Blockhub
{
    public interface ICurrency
    {
        string ToDisplayAmount();
    }

    public interface ICurrency<out T> : ICurrency
        where T : IBlockchainType
    {
        decimal Amount { get; }
        string Unit { get; }
        string Name { get; }

        T TokenType { get; }

        BigInteger ToTransactionAmount();
    }
}