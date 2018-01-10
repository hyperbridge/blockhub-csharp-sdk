using System.Threading.Tasks;

namespace Hyperbridge.Wallet
{
    public interface IBalanceRead<T> where T : ICoinCurrency
    {
        Task<ICoin<T>> GetBalance(IAccount<T> account);
    }
}