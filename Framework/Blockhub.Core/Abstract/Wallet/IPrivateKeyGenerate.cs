using System.Threading.Tasks;

namespace Blockhub.Wallet
{
    public interface IPrivateKeyGenerate<T> where T : ITokenSource
    {
        Task<string> GetPrivateKey(Wallet<T> wallet, string address);
    }
}
