using System.Collections.Generic;
namespace Hyperbridge.Profile
{
    public class ProfileData
    {
        
        public string name;
        public string imageLocation;
        public string uuid;
        public List<Notification> notifications = new List<Notification>();
    }
}