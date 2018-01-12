using Hyperbridge.Services.Abstract;
using System;
using System.Linq;

namespace Hyperbridge.Services.Ethereum
{
    public class EthereumAccountCreator : IAccountCreator
    {
        public Account CreateAccount(Wallet wallet, string name)
        {
            if (wallet == null) throw new ArgumentNullException(nameof(wallet));
            if (string.IsNullOrWhiteSpace(nameof(name))) throw new ArgumentNullException(nameof(name));

            var ethWallet = new Nethereum.NethereumHdWallet(wallet.Secret);
            var ethAccount = ethWallet.GetAccount(++wallet.LastIndexUsed);

            var account = new Account
            {
                Id = Guid.NewGuid().ToString(),
                Address = ethAccount.Address,
                Name = name,
                Wallet = wallet,
                WalletIndex = wallet.LastIndexUsed,
            };

            wallet.Accounts.Add(account);

            return account;
        }
    }
}
