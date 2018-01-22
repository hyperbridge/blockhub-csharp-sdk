using System.Linq;
using Blockhub.Wallet;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Blockhub.Nethereum
{
    [TestClass]
    public class GeneratorTests
    {
        [TestMethod]
        public void Bip39SeedGenerate_12Words()
        {
            var generator = new Bip39SeedGenerate(NBitcoin.Wordlist.English, NBitcoin.WordCount.Twelve);
            var key = generator.Generate();

            Assert.IsNotNull(key);
            Assert.AreEqual(12, key.Split(' ').Count());
        }
    }
}
