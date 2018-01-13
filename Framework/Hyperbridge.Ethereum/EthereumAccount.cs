using Hyperbridge.Wallet;
using Hyperbridge.Data;

namespace Hyperbridge.Nethereum
{
    public class EthereumAccount : IAccount<Ethereum.Ethereum>
    {
        public string Address { get; set; }
        public string PrivateKey { get; set; }

        public IWallet<Ethereum.Ethereum> Wallet { get; set; }
    }
}
