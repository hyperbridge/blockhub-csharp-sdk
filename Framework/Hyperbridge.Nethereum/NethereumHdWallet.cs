using System;
using System.Linq;
using NWallet = Nethereum.HdWallet;
using Hyperbridge.Wallet;
using Hyperbridge.Ethereum;

namespace Hyperbridge.Nethereum
{
    /// <summary>
    /// Nethereum wallet implementation
    /// </summary>
    public class NethereumHdWallet : IWallet<Ether>
    {
        private NWallet.Wallet NetherumWallet { get; }
        private int MaxIndexSearch { get; }

        public NethereumHdWallet(string seed) : this(seed, 20) { }
        public NethereumHdWallet(string seed, int maxIndexSearch)
        {
            if (string.IsNullOrWhiteSpace(seed)) throw new ArgumentNullException(nameof(seed));
            if (maxIndexSearch < 1) throw new ArgumentOutOfRangeException(nameof(maxIndexSearch), $"Maximum iterations to search for address must be at least 1.");

            NetherumWallet = new NWallet.Wallet(seed, null);
            MaxIndexSearch = maxIndexSearch;
        }

        public IAccount<Ether>[] GenerateAccounts(int count, int startIndex = 0)
        {
            if (startIndex < 0) throw new ArgumentOutOfRangeException(nameof(startIndex), "Index must be 0 or greater to generate an account based on index.");

            var indices = Enumerable.Range(startIndex, count);
            return indices.Select(x => new NethereumAccount(NetherumWallet.GetAccount(x))).ToArray();
        }

        public IAccount<Ether> GetAccount(string address)
        {
            // TODO: The need to specify the maximum index search may need to be refactored one day
            //       into a separate search class in the event we discover people creating hundreds or thousands of 
            //       accounts. By default, it only searches the first 20 accounts. We may want to create an algorithm
            //       to better handle this instead of a static number passed into this class. This will provide
            //       a better separation of concerns for this class.
            // NOTE: This is also indicative of a decision about the maximum number of accounts for a given wallet that 
            //       should be allowed (if a limit should be enforced)
            var foundAccount = NetherumWallet.GetAccount(address, MaxIndexSearch);
            if (foundAccount == null) throw new WalletAddressNotFoundException<Ether>(this, address);

            return new NethereumAccount(foundAccount);
        }

        public IAccount<Ether> GetAccount(int index)
        {
            if (index < 0) throw new ArgumentOutOfRangeException(nameof(index), "Index must be 0 or greater to generate an account based on index.");
            return new NethereumAccount(NetherumWallet.GetAccount(index));
        }
    }
}
