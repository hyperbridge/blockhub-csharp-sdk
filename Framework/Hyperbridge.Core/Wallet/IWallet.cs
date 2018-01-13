using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hyperbridge.Wallet
{
    public interface IWallet<T> where T : ITokenSource
    {
        string Secret { get; }
        IEnumerable<IAccount<T>> Accounts { get; }

        IAccount<T> AddAccount(IAccount<T> account);
        IAccount<T> AddAccount(string address, string privateKey);
    }
}
