using Hyperbridge.Transaction;
using Hyperbridge.Wallet;
using System;
using System.Linq;
using System.Text;

namespace Hyperbridge.Services.Abstract
{
    public interface IWalletCreator
    {
        ICoinCurrency Currency { get; }
        Wallet CreateWallet(string secret, string name, string password);
        Wallet CreateWallet(string name, string password);
    }
}
