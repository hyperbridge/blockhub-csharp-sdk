using Hyperbridge.Ethereum;
using Hyperbridge.Services.Abstract;
using Hyperbridge.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hyperbridge.Services.Ethereum
{
    public class EthereumTransactionRead : ITransactionRead
    {
        private ILastTransactionRead<EthereumTransaction> TransactionRead { get; }
        public EthereumTransactionRead(ILastTransactionRead<EthereumTransaction> transactionRead)
        {
            TransactionRead = transactionRead ?? throw new ArgumentNullException(nameof(transactionRead));
        }

        public async Task<IEnumerable<ReceiveTransaction>> GetLastReceivedTransactions(Account account, int page = 1, int limit = 25)
        {
            if (account == null) throw new ArgumentNullException(nameof(account));
            if (page < 1) throw new ArgumentOutOfRangeException(nameof(page));
            if (limit < 1) throw new ArgumentOutOfRangeException(nameof(limit));

            var transactions = await GetTransactionsFiltered(account, page, limit, t => account.Address.Equals(t.ToAddress, StringComparison.OrdinalIgnoreCase));
            return transactions.Select(x => new ReceiveTransaction
            {
                Amount = x.Amount,
                Currency = x.Currency,
                FromAddress = x.FromAddress,
                ToAddress = account,
                TimeStamp = x.TimeStamp,
                Unit = x.Unit
            }).ToArray();
        }

        public async Task<IEnumerable<SendTransaction>> GetLastSentTransactions(Account account, int page = 1, int limit = 25)
        {
            if (account == null) throw new ArgumentNullException(nameof(account));
            if (page < 1) throw new ArgumentOutOfRangeException(nameof(page));
            if (limit < 1) throw new ArgumentOutOfRangeException(nameof(limit));

            var transactions = await GetTransactionsFiltered(account, page, limit, t => account.Address.Equals(t.FromAddress, StringComparison.OrdinalIgnoreCase));
            return transactions.Select(x => new SendTransaction
            {
                Amount = x.Amount,
                Currency = x.Currency,
                FromAddress = account,
                ToAddress = x.ToAddress,
                TimeStamp = x.TimeStamp,
                Unit = x.Unit
            }).ToArray();
        }

        private async Task<IEnumerable<Transaction>> GetTransactionsFiltered(Account account, int page, int limit, Func<EthereumTransaction, bool> predicate)
        {
            var transactions = new List<Transaction>();

            // This algorithm is inefficient. Caching is important here in order to save resources. 
            // We may decide to store transactions locally to avoid retrieving them over and over again.
            var startIndex = (page - 1) * limit + 1;    // 1-Based Index
            var stopIndex = page * limit;               // 1-Based Index

            int searchPage = 1;
            while(transactions.Count < stopIndex)
            {
                // Pulling back twice as many as needed limits the number of round trips to the server.
                // This is assuming a 50/50 split on sending/receiving.
                var remoteTransactions = await TransactionRead.GetLastTransactions(account.Address, searchPage, 2 * limit);
                var matchingTransactions = remoteTransactions.Where(predicate);
                transactions.AddRange(matchingTransactions.Select(ToTransactionConversionFunction));

                // Break early if we didn't get as many records as expected.
                if (remoteTransactions.Count() < 2 * limit) break;

                // Let's look at the next page and pull back as many as possible again until
                // we find the correct number we're looking for.
                searchPage++;
            }

            return transactions.Skip(startIndex - 1).Take(limit).ToArray();
        }

        public async Task<IEnumerable<Transaction>> GetLastTransactions(Account account, int page = 1, int limit = 25)
        {
            if (account == null) throw new ArgumentNullException(nameof(account));
            if (page < 1) throw new ArgumentOutOfRangeException(nameof(page));
            if (limit < 1) throw new ArgumentOutOfRangeException(nameof(limit));

            var transactions = await TransactionRead.GetLastTransactions(account.Address, page, limit);
            return transactions.Select(ToTransactionConversionFunction).ToArray();
        }

        private Transaction ToTransactionConversionFunction(EthereumTransaction transaction)
        {
            return new Transaction
            {
                Amount = ((EtherCoin)transaction.Coin).Amount,
                Currency = Ether.Instance,
                FromAddress = transaction.FromAddress,
                TimeStamp = transaction.TransactionTime,
                ToAddress = transaction.ToAddress,
                Unit = ((EtherCoin)transaction.Coin).Unit
            };
        }
    }
}
