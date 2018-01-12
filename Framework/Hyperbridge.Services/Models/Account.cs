namespace Hyperbridge.Services
{
    public class Account
    {
        public string Id { get; set; }
        public Wallet Wallet { get; set; }

        public int WalletIndex { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        // NOTE: Do not store private key at all anywhere. This can be re-calculated
    }
}
