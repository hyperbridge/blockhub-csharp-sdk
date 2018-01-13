using System;
using Hyperbridge.Wallet;
using System.Collections.Generic;
using Hyperbridge.Ethereum;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Hyperbridge.Data;
using System.Linq;

namespace Hyperbridge.Nethereum
{
    public class EthereumWallet : IWallet<Ethereum.Ethereum>
    {
        private Collection<IAccount<Ethereum.Ethereum>> AccountCollection { get; } = new Collection<IAccount<Ethereum.Ethereum>>();

        public string Secret { get; set; }
        public IEnumerable<IAccount<Ethereum.Ethereum>> Accounts => AccountCollection;

        public IAccount<Ethereum.Ethereum> AddAccount(IAccount<Ethereum.Ethereum> account)
        {
            if (account.Wallet != this) throw new InvalidOperationException("Only accounts created for this wallet can be added.");
            return AddAccount(account.Address, account.PrivateKey);
        }

        public IAccount<Ethereum.Ethereum> AddAccount(string address, string privateKey)
        {
            var account = new EthereumAccount
            {
                Address = address,
                PrivateKey = privateKey,
                Wallet = this
            };

            AccountCollection.Add(account);
            return account;
        }
    }
}
