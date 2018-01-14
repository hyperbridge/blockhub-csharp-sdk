using Newtonsoft.Json;

namespace Blockhub.Services
{
    public class ProfileObject
    {
        /// <summary>
        /// Unique Id of the object
        /// </summary>
        [JsonProperty]
        public string Id { get; set; }

        /// <summary>
        /// Name of the object
        /// </summary>
        [JsonProperty]
        public string Name { get; set; }
    }
}
