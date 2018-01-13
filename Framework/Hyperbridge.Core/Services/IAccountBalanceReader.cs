using System.Threading.Tasks;

namespace Hyperbridge.Services.Abstract
{
    public interface IAccountBalanceReader
    {
        Task<AccountBalance> GetAccountBalance(Account account);
    }
}
