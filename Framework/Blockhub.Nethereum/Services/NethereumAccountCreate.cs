using System;
using System.Linq;
using NWallet = Nethereum.HdWallet;
using Blockhub.Wallet;
using Blockhub.Ethereum;
using System.Threading.Tasks;
using Blockhub.Services;
using N = Nethereum.Web3;

namespace Blockhub.Nethereum
{
    /// <summary>
    /// Nethereum wallet implementation
    /// </summary>
    public class NethereumAccountCreate : IAccountCreate<Ethereum.Ethereum>
    {
        private int MaxIndexSearch { get; }

        public NethereumAccountCreate() : this(20) { }
        public NethereumAccountCreate(int maxIndexSearch)
        {
            if (maxIndexSearch < 1) throw new ArgumentOutOfRangeException(nameof(maxIndexSearch), $"Maximum iterations to search for address must be at least 1.");
            MaxIndexSearch = maxIndexSearch;
        }

        private NWallet.Wallet CreateNetherumWallet(Wallet<Ethereum.Ethereum> wallet)
        {
            return new NWallet.Wallet(wallet.Secret, null);
        }

        public async Task<Account<Ethereum.Ethereum>[]> CreateAccounts(Wallet<Ethereum.Ethereum> wallet, int count, int startIndex = 0)
        {
            if (startIndex < 0) throw new ArgumentOutOfRangeException(nameof(startIndex), "Index must be 0 or greater to generate an account based on index.");

            var nethereumWallet = CreateNetherumWallet(wallet);
            var indices = Enumerable.Range(startIndex, count);
            return indices.Select(x => ConvertAccount(wallet, nethereumWallet.GetAccount(x), "")).ToArray();
        }

        public Task<Account<Ethereum.Ethereum>> CreateAccount(Wallet<Ethereum.Ethereum> wallet, string address)
        {
            return CreateAccount(wallet, address, "");
        }

        public async Task<Account<Ethereum.Ethereum>> CreateAccount(Wallet<Ethereum.Ethereum> wallet, string address, string name)
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
            return ConvertAccount(wallet, foundAccount, name);
        }

        public Task<Account<Ethereum.Ethereum>> CreateAccount(Wallet<Ethereum.Ethereum> wallet, int index)
        {
            return CreateAccount(wallet, index, "");
        }

        public async Task<Account<Ethereum.Ethereum>> CreateAccount(Wallet<Ethereum.Ethereum> wallet, int index, string name)
        {
            if (index < 0) throw new ArgumentOutOfRangeException(nameof(index), "Index must be 0 or greater to generate an account based on index.");
            var nAccount = CreateNetherumWallet(wallet).GetAccount(index);
            return ConvertAccount(wallet, nAccount, name);
        }

        private Account<Ethereum.Ethereum> ConvertAccount(Wallet<Ethereum.Ethereum> wallet, N.Accounts.Account nAccount, string name)
        {
            var account = new Account<Ethereum.Ethereum>
            {
                Id = Guid.NewGuid().ToString(),
                Name = name,
                Address = nAccount.Address,
                Wallet = wallet
            };
            account.SetPrivateKey(nAccount.PrivateKey);

            AddAccountToWallet(wallet, account);

            return account;
        }

        private void AddAccountToWallet(Wallet<Ethereum.Ethereum> wallet, Account<Ethereum.Ethereum> account)
        {
            if (string.IsNullOrEmpty(account.Name))
            {
                var foundAccount = wallet.Accounts
                    .FirstOrDefault(x => x.Name.Equals(account.Name, StringComparison.OrdinalIgnoreCase));
                if (foundAccount != null) throw new AccountNameTakenException(wallet.Id, wallet.Name, account.Id, account.Name);
            }

            var addressFoundAccount = wallet.Accounts
                .FirstOrDefault(x => x.Address.Equals(account.Address, StringComparison.OrdinalIgnoreCase));
            if (addressFoundAccount != null) throw new AccountAddressAlreadyExistsException(wallet.Id, wallet.Name, account.Id, account.Address);

            wallet.Accounts.Add(account);
        }
    }
}
