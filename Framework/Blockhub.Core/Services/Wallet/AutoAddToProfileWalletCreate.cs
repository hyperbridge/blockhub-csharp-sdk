using System;
using System.Linq;
using System.Threading.Tasks;

namespace Blockhub.Wallet
{
    public class AutoAddToProfileWalletCreate<T> : IWalletCreate<T> where T : IBlockchainType
    {
        private IWalletCreate<T> InnerCreate { get; }
        private IProfileContextAccess ProfileAccessor { get; }

        public AutoAddToProfileWalletCreate(IWalletCreate<T> innerCreate, IProfileContextAccess profileAccessor)
        {
            InnerCreate = innerCreate ?? throw new ArgumentNullException(nameof(innerCreate));
            ProfileAccessor = profileAccessor ?? throw new ArgumentNullException(nameof(profileAccessor));
        }

        public async Task<Wallet<T>> CreateWallet(string secret, string name, string password)
        {
            var wallet = await InnerCreate.CreateWallet(secret, name, password);
            AddToProfile(wallet);
            return wallet;
        }

        public async Task<Wallet<T>> CreateWallet(string name, string password)
        {
            var wallet = await InnerCreate.CreateWallet(name, password);
            AddToProfile(wallet);
            return wallet;
        }

        private void AddToProfile(Wallet<T> wallet)
        {
            var profile = ProfileAccessor.Profile ?? throw new ProfileNotSetException();
            var wallets = profile.GetWallets<T>();

            var foundWallet = wallets.FirstOrDefault(x => x.Name.Equals(wallet.Name, StringComparison.OrdinalIgnoreCase));
            if (foundWallet != null) throw new WalletAlreadyExistsInProfileException(foundWallet.Id, foundWallet.Name);

            profile.ProfileObjects.Add(wallet);
        }
    }
}
