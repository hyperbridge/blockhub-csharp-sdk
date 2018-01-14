using Blockhub.Services;
using Blockhub.Wallet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blockhub.Wallet
{
    public class SearchForAccountsByBalanceWalletCreator<T> : IWalletCreate<T> where T : ITokenSource
    {
        private IWalletCreate<T> WalletCreator { get; }
        private IAccountCreate<T> WalletManager { get; }
        private IBalanceRead<T> BalanceReader { get; }
        private int SearchCount { get; }

        public SearchForAccountsByBalanceWalletCreator(IWalletCreate<T> walletCreator, IAccountCreate<T> walletManager, IBalanceRead<T> balanceReader) :
            this(walletCreator, walletManager, balanceReader, 100)
        { }

        public SearchForAccountsByBalanceWalletCreator(IWalletCreate<T> walletCreator, IAccountCreate<T> walletManager, IBalanceRead<T> balanceReader, int searchCount)
        {
            WalletCreator = walletCreator ?? throw new ArgumentNullException(nameof(walletCreator));
            WalletManager = walletManager ?? throw new ArgumentNullException(nameof(walletManager));
            BalanceReader = balanceReader ?? throw new ArgumentNullException(nameof(balanceReader));

            if (searchCount < 1) throw new ArgumentOutOfRangeException(nameof(searchCount), "Search count must be at least one.");
            SearchCount = searchCount;
        }

        public async Task<Wallet<T>> CreateWallet(string secret, string name, string password)
        {
            return await SearchAndFillInAccounts(await WalletCreator.CreateWallet(secret, name, password));
        }

        public async Task<Wallet<T>> CreateWallet(string name, string password)
        {
            return await SearchAndFillInAccounts(await WalletCreator.CreateWallet(name, password));
        }

        private async Task<Wallet<T>> SearchAndFillInAccounts(Wallet<T> wallet)
        {
            var accountsToSearch = await WalletManager.CreateAccounts(wallet, SearchCount);

            int foundCount = 0;
            for(int i = 0; i < accountsToSearch.Length; i++)
            {
                var account = accountsToSearch[i];
                var balance = await BalanceReader.GetBalance(account);
                if (balance.ToTransactionAmount() > 0)
                {
                    // Let's make sure this account doesn't exist for any reason in the wallet already
                    if (!wallet.Accounts.Any(x => x.Address.Equals(account.Address, StringComparison.OrdinalIgnoreCase)))
                    {
                        foundCount++;
                        wallet.Accounts.Add(account);
                    }
                }
            }

            return wallet;
        }
    }
}
