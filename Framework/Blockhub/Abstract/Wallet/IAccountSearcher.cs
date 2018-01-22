using System.Threading.Tasks;

namespace Blockhub.Wallet
{
    public interface IAccountSearcher<T> where T : IBlockchainType
    {
       Task<Account<T>[]> SearchForAccounts(Wallet<T> wallet, int maxSearchCount);
       Task<Account<T>[]> SearchForAccounts(Wallet<T> wallet, int maxSearchCount, int startIndex);
    }
}
