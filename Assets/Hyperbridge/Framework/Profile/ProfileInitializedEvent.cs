using System.Collections.Generic;
namespace Hyperbridge.Profile
{
    public class ProfileInitializedEvent : CodeControl.Message
    {

        public ProfileData activeProfile;
        public List<ProfileData> profiles;
    }
}


