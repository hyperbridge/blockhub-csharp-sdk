using Newtonsoft.Json;

namespace Blockhub.Wallet
{
    public class Account<T> : ProfileObject where T : ITokenSource
    {
        public string Address { get; set; }

        // TODO: Do we need to hide this?
        public string PrivateKey { get; set; }
    }
}
