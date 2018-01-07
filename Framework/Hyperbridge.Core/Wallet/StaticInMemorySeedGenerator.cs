using System;

namespace Hyperbridge.Wallet
{
    public class StaticInMemorySeedGenerator : ISeedGenerator<string>
    {
        private string Seed { get; }

        public StaticInMemorySeedGenerator(string seed)
        {
            Seed = seed ?? throw new ArgumentNullException(nameof(seed));
        }

        public string Generate()
        {
            return Seed;
        }
    }
}