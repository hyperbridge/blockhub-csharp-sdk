using System;
using Hyperbridge.Wallet;
using Hyperbridge.Services.Abstract;
using System.Threading.Tasks;
using Hyperbridge.Ethereum;
using System.Linq;

namespace Hyperbridge.Nethereum
{
    public class EthereumWalletCreate : IWalletCreate<Ethereum.Ethereum>
    {
        private ISeedGenerator<string> SeedGenerator { get; }
        public EthereumWalletCreate(ISeedGenerator<string> seedGenerator)
        {
            SeedGenerator = seedGenerator ?? throw new ArgumentNullException(nameof(seedGenerator));
        }

        public async Task<IWallet<Ethereum.Ethereum>> CreateWallet(string secret, string name, string password)
        {
            return new EthereumWallet
            {
                Secret = secret
            };
        }

        public Task<IWallet<Ethereum.Ethereum>> CreateWallet(string name, string password)
        {
            var seed = SeedGenerator.Generate();
            return CreateWallet(seed, name, password);
        }
    }
}
