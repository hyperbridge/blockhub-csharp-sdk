using Newtonsoft.Json;

namespace Blockhub.Wallet
{
    public class Account<T> : ProfileObject where T : IBlockchainType
    {
        public Wallet<T> Wallet { get; set; }
        public string Address { get; set; }

        // HACK: JSON.NET can't handle Reference Handling and JsonIgnore
        private string _PrivateKey;
        public void SetPrivateKey(string value)
        {
            _PrivateKey = value;
        }

        public string GetPrivateKey()
        {
            return _PrivateKey;
        }
    }
}
