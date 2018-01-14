using Blockhub.Wallet;
using System;

namespace Blockhub
{
    public class InvalidPrivateKeyException : Exception
    {
        public string Address { get; }
        public InvalidPrivateKeyException(string address) : base($"Invalid private key for account '{address}'.")
        {
            Address = address;
        }
    }

    public class InvalidPrivateKeyException<T> : InvalidPrivateKeyException where T : ITokenSource
    {
        public Account<T> Account { get; }
        public InvalidPrivateKeyException(Account<T> account) : base(account?.Address)
        {
            Account = account;
        }
    }
}
