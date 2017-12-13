
using System.Collections.Generic;
namespace Hyperbridge.Profile
{
    public class EditProfileEvent : CodeControl.Message
    {
        public ProfileData profileToEdit;
        public string originalProfileName;
        public string newProfileName;
        public bool deleteProfile;
    }
}
