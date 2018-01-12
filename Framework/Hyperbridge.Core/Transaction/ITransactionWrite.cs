﻿using Hyperbridge.Wallet;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hyperbridge.Transaction
{
    public interface ITransactionWrite<T> where T : ICoinCurrency
    {
        Task<TransactionSentResponse<T>> SendTransactionAsync(IAccount<T> fromAccount, IAccount<T> toAccount, ICoin<T> amount);
    }
}
