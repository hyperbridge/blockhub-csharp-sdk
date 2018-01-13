using System;
using System.Threading.Tasks;

namespace Hyperbridge.Wallet
{
    public interface IAccount<T> where T : ITokenSource
    {
        string Address { get; }
        string PrivateKey { get; }

        IWallet<T> Wallet { get; }
    }
}
