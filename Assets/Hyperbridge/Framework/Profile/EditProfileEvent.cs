
using System.Collections.Generic;
namespace Hyperbridge.Profile
{
    public class EditProfileEvent : CodeControl.Message
    {
        public string imageLocation;
        public string name;
        public bool deleteProfile;
        public List<Notification> notifications;
    }
}
