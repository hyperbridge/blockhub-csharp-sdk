using System;
using System.Numerics;

namespace Blockhub.Transaction
{
    public interface ITransaction
    {
        DateTime TransactionTime { get; }
        string FromAddress { get; }
        string ToAddress { get; }
        BigInteger Amount { get; }
    }
}
