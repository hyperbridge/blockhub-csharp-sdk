using Blockhub.Data;
using System;
using System.Threading.Tasks;

namespace Blockhub.Wallet
{
    public class AutoSaveProfileWalletCreate<T> : IWalletCreate<T> where T : ITokenSource
    {
        private IWalletCreate<T> InnerCreate { get; }
        private IProfileContextAccessor ProfileAccessor { get; }
        private ISaver<Profile> ProfileSaver { get; }

        public AutoSaveProfileWalletCreate(IWalletCreate<T> innerCreate, IProfileContextAccessor profileAccessor, ISaver<Profile> profileSaver)
        {
            InnerCreate = innerCreate ?? throw new ArgumentNullException(nameof(innerCreate));
            ProfileAccessor = profileAccessor ?? throw new ArgumentNullException(nameof(profileAccessor));
            ProfileSaver = profileSaver ?? throw new ArgumentNullException(nameof(profileSaver));
        }

        public async Task<Wallet<T>> CreateWallet(string secret, string name, string password)
        {
            var wallet = await InnerCreate.CreateWallet(secret, name, password);
            await SaveProfile();
            return wallet;
        }

        public async Task<Wallet<T>> CreateWallet(string name, string password)
        {
            var wallet = await InnerCreate.CreateWallet(name, password);
            await SaveProfile();
            return wallet;
        }

        private async Task SaveProfile()
        {
            var profile = ProfileAccessor.Profile;
            if (profile == null) throw new ProfileNotSetException();

            profile.ProfileUri = await ProfileSaver.Save(profile);
        }
    }
}
