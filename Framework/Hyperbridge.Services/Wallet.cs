using Hyperbridge.Wallet;
using Newtonsoft.Json;

namespace Hyperbridge.Services
{
    public class Wallet
    {
        [JsonConverter(typeof(ICoinCurrencyConverter))]
        public ICoinCurrency BlockchainType { get; internal set; }

        public string Id { get; internal set; }
        public string Name { get; internal set; }
        public string Secret { get; internal set; }

        // In order to properly create new accounts without re-using any that have already
        // been created in the past, we should keep track of the last index used to create
        // an account.
        public int LastIndexUsed { get; internal set; }
        // NOTE: Do not store password at all anywhere

        public Account[] Accounts { get; internal set; }
    }
}
