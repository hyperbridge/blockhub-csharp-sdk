using Blockhub.Services;
using System.Threading.Tasks;

namespace Blockhub.Wallet
{
    public interface IBalanceRead<T> where T : IBlockchainType
    {
        Task<ICurrency<T>> GetBalance(Account<T> account);
    }
}