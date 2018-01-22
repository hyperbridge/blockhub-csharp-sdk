using Blockhub.Data;
using System;
using System.Threading.Tasks;

namespace Blockhub
{
    public class LoadProfileContextLoad : ILoad<Profile>
    {
        private ILoad<Profile> InnerLoad { get; }
        private IProfileContextAccess ProfileAccessor { get; }
        public LoadProfileContextLoad(ILoad<Profile> innerLoad, IProfileContextAccess profileAccessor)
        {
            InnerLoad = innerLoad ?? throw new ArgumentNullException(nameof(innerLoad));
            ProfileAccessor = profileAccessor ?? throw new ArgumentNullException(nameof(profileAccessor));
        }

        public async Task<Profile> Load(string uri)
        {
            var loadedProfile = await InnerLoad.Load(uri);
            ProfileAccessor.Profile = loadedProfile;
            return loadedProfile;
        }
    }
}
