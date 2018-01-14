using Blockhub.Transaction;
using Blockhub.Wallet;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blockhub.Wallet
{
    public interface IWalletCreate<T> where T : ITokenSource
    {
        Task<Wallet<T>> CreateWallet(string secret, string name, string password);
        Task<Wallet<T>> CreateWallet(string name, string password);
    }
}
