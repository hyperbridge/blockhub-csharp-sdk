using NWallet = Nethereum.HdWallet;
using Blockhub.Wallet;
using System.Threading.Tasks;

namespace Blockhub.Nethereum
{
    public class NethereumPrivateKeyGenerate : IPrivateKeyGenerate<Ethereum.Ethereum>
    {
        public Task<string> GetPrivateKey(Wallet<Ethereum.Ethereum> wallet, string address)
        {
            var nAccount = CreateNetherumWallet(wallet).GetAccount(address);
            return Task.FromResult(nAccount.PrivateKey);
        }

        private NWallet.Wallet CreateNetherumWallet(Wallet<Ethereum.Ethereum> wallet)
        {
            return new NWallet.Wallet(wallet.Secret, null);
        }
    }
}
