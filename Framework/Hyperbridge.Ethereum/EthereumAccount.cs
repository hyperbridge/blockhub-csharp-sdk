using Blockhub.Wallet;
using Blockhub.Data;

namespace Blockhub.Nethereum
{
    public class EthereumAccount : IAccount<Ethereum.Ethereum>
    {
        public string Address { get; set; }
        public string PrivateKey { get; set; }

        public IWallet<Ethereum.Ethereum> Wallet { get; set; }
    }
}
