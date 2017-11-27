

namespace Hyperbridge.Profile
{
    public class EditProfileEvent : CodeControl.Message
    {
        public string imageLocation;
        public string profileName;
        public bool makeDefault;
    }
}
