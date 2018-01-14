using Blockhub.Wallet;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Blockhub
{
    public class Profile : ProfileObject
    {
        /// <summary>
        /// Uri to the profile image to be shown in the UI
        /// </summary>
        public string ImageUri { get; set;  }

        /// <summary>
        /// Notifications shown for this profile
        /// </summary>
        public List<Notification> Notifications { get; set; }

        [JsonProperty]
        public ICollection<ProfileObject> ProfileObjects { get; } = new Collection<ProfileObject>();

        public IEnumerable<Wallet<T>> GetWallets<T>() where T : ITokenSource
        {
            return GetObjects<Wallet<T>>();
        }

        public IEnumerable<T> GetObjects<T>() where T : ProfileObject
        {
            return ProfileObjects
                .Where(x => x is T)
                .Select(x => (T) x)
                .ToArray();
        }
    }
}
