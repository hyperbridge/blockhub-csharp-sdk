using System;
using System.Linq;
using NWallet = Nethereum.HdWallet;
using Hyperbridge.Wallet;

namespace Hyperbridge.Nethereum
{
    /// <summary>
    /// Nethereum wallet implementation
    /// </summary>
    public class NethereumHdWallet : IWallet<NethereumAccount>
    {
        private readonly NWallet.Wallet _Wallet;

        public NethereumHdWallet(string seedWords)
        {
            if (string.IsNullOrWhiteSpace(seedWords)) throw new ArgumentNullException(nameof(seedWords));
            _Wallet = new NWallet.Wallet(seedWords, null);
        }

        public NethereumAccount[] GenerateAccounts(int count, int startIndex = 0)
        {
            if (startIndex < 0) throw new ArgumentOutOfRangeException(nameof(startIndex), "Index must be 0 or greater to generate an account based on index.");

            var indices = Enumerable.Range(startIndex, count);
            return indices.Select(x => new NethereumAccount(_Wallet.GetAccount(x))).ToArray();
        }

        public NethereumAccount GetAccount(string address)
        {
            var foundAccount = _Wallet.GetAccount(address);
            if (foundAccount == null) throw new WalletAddressNotFoundException<NethereumAccount>(this, address);

            return new NethereumAccount(foundAccount);
        }

        public NethereumAccount GetAccount(int index)
        {
            if (index < 0) throw new ArgumentOutOfRangeException(nameof(index), "Index must be 0 or greater to generate an account based on index.");
            return new NethereumAccount(_Wallet.GetAccount(index));
        }
    }
}
