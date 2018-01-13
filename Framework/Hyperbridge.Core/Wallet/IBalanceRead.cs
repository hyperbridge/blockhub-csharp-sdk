using System.Threading.Tasks;

namespace Hyperbridge.Wallet
{
    public interface IBalanceRead<T> where T : ITokenSource
    {
        Task<IToken<T>> GetBalance(IAccount<T> account);
    }
}