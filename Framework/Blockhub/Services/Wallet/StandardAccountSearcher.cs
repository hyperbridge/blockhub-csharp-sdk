using Blockhub.Services;
using Blockhub.Wallet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blockhub.Wallet
{
    public class StandardAccountSearcher<T> : IAccountSearcher<T> where T : IBlockchainType
    {
        private IAccountCreate<T> AccountCreator { get; }
        private IBalanceRead<T> BalanceReader { get; }

        public StandardAccountSearcher(IAccountCreate<T> accountCreator, IBalanceRead<T> balanceReader)
        {
            AccountCreator = accountCreator ?? throw new ArgumentNullException(nameof(accountCreator));
            BalanceReader = balanceReader ?? throw new ArgumentNullException(nameof(balanceReader));
        }

        public Task<Account<T>[]> SearchForAccounts(Wallet<T> wallet, int maxSearchCount)
        {
            return SearchForAccounts(wallet, maxSearchCount, 0);
        }

        public async Task<Account<T>[]> SearchForAccounts(Wallet<T> wallet, int maxSearchCount, int startIndex)
        {
            var accountsToSearch = await AccountCreator.CreateAccounts(wallet, maxSearchCount, startIndex);

            List<Account<T>> foundAccounts = new List<Account<T>>();
            for(int i = 0; i < accountsToSearch.Length; i++)
            {
                var account = accountsToSearch[i];
                var balance = await BalanceReader.GetBalance(account);
                if (balance.ToTransactionAmount().LongValue > 0)
                {
                    foundAccounts.Add(account);
                }
            }

            return foundAccounts.ToArray();
        }
    }
}
