using System;

namespace Blockhub
{
    public class AccountAddressAlreadyExistsException : Exception
    {
        public string AccountId { get; }
        public string AccountAddress { get; }
        public string WalletId { get; }
        public string WalletName { get; }

        public AccountAddressAlreadyExistsException(string walletId, string walletName, string accountId, string accountAddress) :
            base($"Account address '{accountAddress}' already exists in wallet '{walletName}' [Account Id = {accountId}].")
        {
            WalletId = walletId;
            WalletName = walletName;
            AccountId = accountId;
            AccountAddress = accountAddress;
        }
    }
}
