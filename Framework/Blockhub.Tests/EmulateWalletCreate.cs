using Blockhub.Wallet;
using System.Threading.Tasks;

namespace Blockhub.Tests
{
    class EmulateWalletCreate<T> : IWalletCreate<T>
        where T : IBlockchainType
    {
        public EmulateWalletCreate(Wallet<T> wallet)
        {
            Wallet = wallet;
        }

        public Wallet<T> Wallet { get; }

        public Task<Wallet<T>> CreateWallet(string secret, string name, string password)
        {
            return Task.FromResult(Wallet);
        }

        public Task<Wallet<T>> CreateWallet(string name, string password)
        {
            return Task.FromResult(Wallet);
        }
    }
}
