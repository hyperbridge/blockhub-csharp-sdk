using NBitcoin.BouncyCastle.Math;
using System;

namespace Blockhub.Transaction
{
    public interface ITransaction
    {
        DateTime TransactionTime { get; }
        string FromAddress { get; }
        string ToAddress { get; }
        BigInteger GetAmount();
    }
}
