using Blockhub.Transaction;
using Blockhub.Wallet;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blockhub.Services.Abstract
{
    public interface IWalletCreate<T> where T : ITokenSource
    {
        Task<IWallet<T>> CreateWallet(string secret, string name, string password);
        Task<IWallet<T>> CreateWallet(string name, string password);
    }
}
