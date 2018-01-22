using System;

namespace Blockhub
{
    public class WalletAlreadyExistsInProfileException : Exception
    {
        public string WalletId { get; }
        public string WalletName { get; }

        public WalletAlreadyExistsInProfileException(string id, string name) : 
            base($"The wallet with the name '{name}' already exists in the profile [Wallet Id = {id}].")
        {
            WalletId = id;
            WalletName = name;
        }
    }
}
