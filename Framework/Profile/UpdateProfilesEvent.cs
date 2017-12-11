using System.Collections.Generic;

namespace Hyperbridge.Profile
{
    public class UpdateProfilesEvent : CodeControl.Message
    {
        public List<ProfileData> profiles;
        public ProfileData activeProfile;
    }
}
