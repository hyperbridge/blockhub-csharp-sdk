using System;

namespace Hyperbridge.Wallet
{
    public interface ICoin<T> where T : ICoinCurrency
    {
        decimal Amount { get; }
        T BaseCurrency { get; }

        decimal ToTransactionAmount();
    }
}