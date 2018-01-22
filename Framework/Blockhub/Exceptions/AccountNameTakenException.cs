using System;

namespace Blockhub
{
    public class AccountNameTakenException : Exception
    {
        public string AccountId { get; }
        public string AccountName { get; }
        public string WalletId { get; }
        public string WalletName { get; }

        public AccountNameTakenException(string walletId, string walletName, string accountId, string accountName) : 
            base($"Account name '{accountName}' already exists in wallet '{walletName}' [Account Id = {accountId}].")
        {
            WalletId = walletId;
            WalletName = walletName;
            AccountId = accountId;
            AccountName = accountName;
        }
    }
}
