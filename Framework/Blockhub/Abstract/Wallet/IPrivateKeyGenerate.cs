using System.Threading.Tasks;

namespace Blockhub.Wallet
{
    public interface IPrivateKeyGenerate<T> where T : IBlockchainType
    {
        Task<string> GetPrivateKey(Wallet<T> wallet, string address);
    }
}
