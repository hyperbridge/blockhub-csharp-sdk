using Blockhub.Services;
using System.Threading.Tasks;

namespace Blockhub.Wallet
{
    public interface IBalanceRead<T> where T : ITokenSource
    {
        Task<IToken<T>> GetBalance(Account<T> account);
    }
}