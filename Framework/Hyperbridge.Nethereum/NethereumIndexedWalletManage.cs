using System;
using System.Linq;
using NWallet = Nethereum.HdWallet;
using Blockhub.Wallet;
using Blockhub.Ethereum;
using System.Threading.Tasks;

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

        private NWallet.Wallet CreateNetherumWallet(IWallet<Ethereum.Ethereum> wallet)
        {
            return new NWallet.Wallet(wallet.Secret, null);
        }

        public async Task<IAccount<Ethereum.Ethereum>[]> GenerateAccounts(IWallet<Ethereum.Ethereum> wallet, int count, int startIndex = 0)
        {
            if (startIndex < 0) throw new ArgumentOutOfRangeException(nameof(startIndex), "Index must be 0 or greater to generate an account based on index.");

            var nethereumWallet = CreateNetherumWallet(wallet);
            var indices = Enumerable.Range(startIndex, count);
            return indices.Select(x =>
            {
                var account = nethereumWallet.GetAccount(x);
                return new EthereumAccount
                {
                    Address = account.Address,
                    PrivateKey = account.PrivateKey,
                    Wallet = wallet
                };
            }).ToArray();
        }

        public async Task<IAccount<Ethereum.Ethereum>> GetAccount(IWallet<Ethereum.Ethereum> wallet, string address)
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

            return new EthereumAccount
            {
                Address = foundAccount.Address,
                PrivateKey = foundAccount.PrivateKey,
                Wallet = wallet
            };
        }

        public async Task<IAccount<Ethereum.Ethereum>> GetAccount(IWallet<Ethereum.Ethereum> wallet, int index)
        {
            if (index < 0) throw new ArgumentOutOfRangeException(nameof(index), "Index must be 0 or greater to generate an account based on index.");
            var account = CreateNetherumWallet(wallet).GetAccount(index);
            return new EthereumAccount
            {
                Address = account.Address,
                PrivateKey = account.PrivateKey,
                Wallet = wallet
            };
        }
    }
}
