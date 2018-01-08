using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hyperbridge.Profile
{
    public class Profile
    {
        /// <summary>
        /// Unique Id of the profile
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Name of the Profile to be shown in the UI
        /// </summary>
        public string Name { get; set;  }

        /// <summary>
        /// Uri to the profile image to be shown in the UI
        /// </summary>
        public string ImageUri { get; set;  }

        /// <summary>
        /// Notifications shown for this profile
        /// </summary>
        public List<Notification> Notifications { get; set; }

        /// <summary>
        /// List of wallets this profile currently holds
        /// </summary>
        public List<ProfileWallet> Wallets { get; set; }
    }

    public class ProfileWallet
    {
        public string Id { get; set; }
        public string Type { get; set; }

        public Dictionary<string, string> Parameters { get; set; }
    }
}
