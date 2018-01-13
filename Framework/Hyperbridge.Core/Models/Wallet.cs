using Hyperbridge.Wallet;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Hyperbridge.Services
{
    public class Wallet
    {
        [JsonProperty]
        [JsonConverter(typeof(ITokenSourceConverter))]
        public ITokenSource BlockchainType { get; internal set; }

        [JsonProperty]
        public string Id { get; internal set; }

        [JsonProperty]
        public string Name { get; internal set; }

        [JsonProperty]
        public string Secret { get; internal set; }

        // In order to properly create new accounts without re-using any that have already
        // been created in the past, we should keep track of the last index used to create
        // an account.
        [JsonProperty]
        public int LastIndexUsed { get; internal set; }
        // NOTE: Do not store password at all anywhere

        [JsonProperty]
        public List<Account> Accounts { get; } = new List<Account>();
    }
}
