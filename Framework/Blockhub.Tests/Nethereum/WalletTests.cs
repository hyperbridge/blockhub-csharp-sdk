using System;
using System.Threading.Tasks;
using Blockhub.Services;
using Blockhub.Wallet;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NBitcoin;

namespace Blockhub.Nethereum
{

    [TestClass]
    public class WalletTests
    {
        private readonly string Seed = "candy maple cake bread pudding cream honey grace smooth crumble sweet blanket";

        [TestMethod]
        public async Task WalletsFirstAddressIsConsistent()
        {
            var manager = new NethereumAccountCreate();
            var wallet = new Wallet<Ethereum.Ethereum>
            {
                Secret = Seed
            };

            var firstAccount = await manager.CreateAccount(wallet, 0);
            Assert.AreEqual("0x0ae2415d3a45ea9b009a75643f99a7f88a40b2a3", firstAccount.Address, true);
        }

        [TestMethod]
        public void Generate12WordMnemonicPhrase()
        {
            var generator = new Bip39SeedGenerate(NBitcoin.Wordlist.English, WordCount.Twelve);
            var phrase = generator.Generate();

            var words = phrase.Split(' ');
            Assert.AreEqual(12, words.Length);

            Console.WriteLine($"Phrase: {phrase}");
        }
    }
}
