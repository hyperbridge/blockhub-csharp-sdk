using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NBitcoin;

namespace Hyperbridge.Nethereum
{
    [TestClass]
    public class WalletTests
    {
        private readonly string Seed = "candy maple cake bread pudding cream honey grace smooth crumble sweet blanket";

        [TestMethod]
        public void WalletsFirstAddressIsConsistent()
        {
            var wallet = new NethereumHdWallet(Seed);

            var firstAccount = wallet.GetAccount(0);
            Assert.AreEqual("0x0ae2415d3a45ea9b009a75643f99a7f88a40b2a3", firstAccount.Address, true);
        }

        [TestMethod]
        public void GetAccountByAddressReturnsIdenticalInformationBasedOnIndex()
        {
            var wallet = new NethereumHdWallet(Seed);
            var account = wallet.GetAccount(15);
            var foundAccount = wallet.GetAccount(account.Address);

            Assert.AreEqual(account.Address, foundAccount.Address, true);
            Assert.AreEqual(account.PrivateKey, foundAccount.PrivateKey, true);
        }

        // NOTE: This test takes longer than 10 seconds to run due to the large number of accounts to search.
        //       Can reduce the number as necessary.
        [TestMethod]
        public void GetAccountByAddressReturnsIdenticalInformationBasedOnIndex_LargeIndexValue()
        {
            var wallet = new NethereumHdWallet(Seed, 2000);
            var account = wallet.GetAccount(1500);
            var foundAccount = wallet.GetAccount(account.Address);

            Assert.AreEqual(account.Address, foundAccount.Address, true);
            Assert.AreEqual(account.PrivateKey, foundAccount.PrivateKey, true);
        }

        [TestMethod]
        public void Generate12WordMnemonicPhrase()
        {
            var generator = new Bip39SeedGenerator(NBitcoin.Wordlist.English, WordCount.Twelve);
            var phrase = generator.Generate();

            var words = phrase.Split(' ');
            Assert.AreEqual(12, words.Length);

            Console.WriteLine($"Phrase: {phrase}");
        }
    }
}
