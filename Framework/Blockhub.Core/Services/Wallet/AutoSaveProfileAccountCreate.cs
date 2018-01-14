using Blockhub.Data;
using System;
using System.Threading.Tasks;

namespace Blockhub.Wallet
{
    public class AutoSaveProfileAccountCreate<T> : IAccountCreate<T> where T : ITokenSource
    {
        private IAccountCreate<T> InnerCreate { get; }
        private IProfileContextAccessor ProfileAccessor { get; }
        private ISaver<Profile> ProfileSaver { get; }

        public AutoSaveProfileAccountCreate(IAccountCreate<T> innerCreate, IProfileContextAccessor profileAccessor, ISaver<Profile> profileSaver)
        {
            InnerCreate = innerCreate ?? throw new ArgumentNullException(nameof(innerCreate));
            ProfileAccessor = profileAccessor ?? throw new ArgumentNullException(nameof(profileAccessor));
            ProfileSaver = profileSaver ?? throw new ArgumentNullException(nameof(profileSaver));
        }

        public async Task<Account<T>> CreateAccount(Wallet<T> wallet, string address)
        {
            var result = await InnerCreate.CreateAccount(wallet, address);
            await SaveProfile();
            return result;
        }

        public async Task<Account<T>> CreateAccount(Wallet<T> wallet, string address, string name)
        {
            var result = await InnerCreate.CreateAccount(wallet, address, name);
            await SaveProfile();
            return result;
        }

        public async Task<Account<T>> CreateAccount(Wallet<T> wallet, int index)
        {
            var result = await InnerCreate.CreateAccount(wallet, index);
            await SaveProfile();
            return result;
        }

        public async Task<Account<T>> CreateAccount(Wallet<T> wallet, int index, string name)
        {
            var result = await InnerCreate.CreateAccount(wallet, index, name);
            await SaveProfile();
            return result;
        }

        public async Task<Account<T>[]> CreateAccounts(Wallet<T> wallet, int count, int startIndex = 0)
        {
            var result = await InnerCreate.CreateAccounts(wallet, count, startIndex);
            await SaveProfile();
            return result;
        }

        private async Task SaveProfile()
        {
            var profile = ProfileAccessor.Profile;
            if (profile == null) throw new ProfileNotSetException();

            profile.ProfileUri = await ProfileSaver.Save(profile);
        }
    }
}
