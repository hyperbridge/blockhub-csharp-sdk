using System;
using System.Linq;
using NWallet = Nethereum.HdWallet;
using Blockhub.Wallet;
using Blockhub.Ethereum;
using System.Threading.Tasks;
using Blockhub.Services;

namespace Blockhub.Nethereum
{
    /// <summary>
    /// Nethereum wallet implementation
    /// </summary>
    public class NethereumIndexedWalletManage : IIndexedWalletManage<Ethereum.Ethereum>
    {
        private int MaxIndexSearch { get; }

        public NethereumIndexedWalletManage() : this(20) { }
        public NethereumIndexedWalletManage(int maxIndexSearch)
        {
            if (maxIndexSearch < 1) throw new ArgumentOutOfRangeException(nameof(maxIndexSearch), $"Maximum iterations to search for address must be at least 1.");
            MaxIndexSearch = maxIndexSearch;
        }

        private NWallet.Wallet CreateNetherumWallet(Wallet<Ethereum.Ethereum> wallet)
        {
            return new NWallet.Wallet(wallet.Secret, null);
        }

        public async Task<Account<Ethereum.Ethereum>[]> GenerateAccounts(Wallet<Ethereum.Ethereum> wallet, int count, int startIndex = 0)
        {
            if (startIndex < 0) throw new ArgumentOutOfRangeException(nameof(startIndex), "Index must be 0 or greater to generate an account based on index.");

            var nethereumWallet = CreateNetherumWallet(wallet);
            var indices = Enumerable.Range(startIndex, count);
            return indices.Select(x =>
            {
                var nAccount = nethereumWallet.GetAccount(x);
                var account = new Account<Ethereum.Ethereum>
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "",
                    Address = nAccount.Address,
                    Wallet = wallet
                };
                account.SetPrivateKey(nAccount.PrivateKey);

                return account;
            }).ToArray();
        }

        public async Task<Account<Ethereum.Ethereum>> GetAccount(Wallet<Ethereum.Ethereum> wallet, string address)
        {
            // TODO: The need to specify the maximum index search may need to be refactored one day
            //       into a separate search class in the event we discover people creating hundreds or thousands of 
            //       accounts. By default, it only searches the first 20 accounts. We may want to create an algorithm
            //       to better handle this instead of a static number passed into this class. This will provide
            //       a better separation of concerns for this class.
            // NOTE: This is also indicative of a decision about the maximum number of accounts for a given wallet that 
            //       should be allowed (if a limit should be enforced)
            var foundAccount = CreateNetherumWallet(wallet).GetAccount(address, MaxIndexSearch);
            if (foundAccount == null) throw new WalletAddressNotFoundException<Ethereum.Ethereum>(this, address);

            var account = new Account<Ethereum.Ethereum>
            {
                Id = Guid.NewGuid().ToString(),
                Name = "",
                Address = foundAccount.Address,
                Wallet = wallet
            };
            account.SetPrivateKey(foundAccount.PrivateKey);

            return account;
        }

        public async Task<Account<Ethereum.Ethereum>> GetAccount(Wallet<Ethereum.Ethereum> wallet, int index)
        {
            if (index < 0) throw new ArgumentOutOfRangeException(nameof(index), "Index must be 0 or greater to generate an account based on index.");
            var nAccount = CreateNetherumWallet(wallet).GetAccount(index);

            var account = new Account<Ethereum.Ethereum>
            {
                Id = Guid.NewGuid().ToString(),
                Name = "",
                Address = nAccount.Address,
                Wallet = wallet
            };
            account.SetPrivateKey(nAccount.PrivateKey);

            return account;
        }

        public async Task<string> GetPrivateKey(Wallet<Ethereum.Ethereum> wallet, string address)
        {
            var nAccount = CreateNetherumWallet(wallet).GetAccount(address);
            return nAccount.PrivateKey;
        }
    }
}
