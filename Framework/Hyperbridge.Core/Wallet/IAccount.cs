using System;
using System.Threading.Tasks;

namespace Blockhub.Wallet
{
    public interface IAccount<T> where T : ITokenSource
    {
        string Address { get; }
        string PrivateKey { get; }

        IWallet<T> Wallet { get; }
    }
}
