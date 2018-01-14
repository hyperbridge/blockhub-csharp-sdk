using Blockhub.Wallet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blockhub
{
    public class WalletAddressNotFoundException : Exception
    {
        public string Address { get; }

        public WalletAddressNotFoundException(string address) : base($"Address '{address}' not found.")
        {
            Address = address ?? throw new ArgumentNullException(nameof(address));
        }

        public WalletAddressNotFoundException(string address, Exception innerException) : base($"Address '{address}' not found.", innerException)
        {
            Address = address ?? throw new ArgumentNullException(nameof(address));
        }
    }

    public class WalletAddressNotFoundException<T> : WalletAddressNotFoundException where T : ITokenSource
    {
        public IAccountCreate<T> Wallet { get; }

        public WalletAddressNotFoundException(IAccountCreate<T> wallet, string address) : base(address)
        {
            Wallet = wallet ?? throw new ArgumentNullException(nameof(wallet));
        }

        public WalletAddressNotFoundException(IAccountCreate<T> wallet, string address, Exception innerException) : base(address, innerException)
        {
            Wallet = wallet ?? throw new ArgumentNullException(nameof(wallet));
        }
    }
}
