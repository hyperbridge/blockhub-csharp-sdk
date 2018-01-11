using Hyperbridge.Wallet;
using System;

namespace Hyperbridge.Ethereum
{
    public class ToEthereumAccount : IAccount<Ether>
    {
        public ToEthereumAccount(string address)
        {
            Address = address ?? throw new ArgumentNullException(nameof(address));
        }

        public string Address { get; }
        public string PrivateKey => throw new NotImplementedException();
    }
}
