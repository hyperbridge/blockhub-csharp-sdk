using Newtonsoft.Json;

namespace Blockhub.Wallet
{
    public class Account<T> : ProfileObject where T : ITokenSource
    {
        public string Address { get; set; }

        [JsonIgnore]
        public string PrivateKey { get; set; }
    }
}
