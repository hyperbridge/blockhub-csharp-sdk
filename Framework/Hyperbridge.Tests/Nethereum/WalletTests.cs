using System;
using System.Threading.Tasks;
using Blockhub.Services;
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
            var manager = new NethereumIndexedWalletManage();
            var wallet = new Wallet<Ethereum.Ethereum>
            {
                Secret = Seed
            };

            var firstAccount = await manager.GetAccount(wallet, 0);
            Assert.AreEqual("0x0ae2415d3a45ea9b009a75643f99a7f88a40b2a3", firstAccount.Address, true);
        }

        [TestMethod]
        public async Task GetAccountByAddressReturnsIdenticalInformationBasedOnIndex()
        {
            var manager = new NethereumIndexedWalletManage();
            var wallet = new Wallet<Ethereum.Ethereum>
            {
                Secret = Seed
            };

            var account = await manager.GetAccount(wallet, 15);
            var foundAccount = await manager.GetAccount(wallet, account.Address);

            Assert.AreEqual(account.Address, foundAccount.Address, true);
            Assert.AreEqual(account.PrivateKey, foundAccount.PrivateKey, true);
        }

        // NOTE: This test takes longer than 10 seconds to run due to the large number of accounts to search.
        //       Can reduce the number as necessary.
        [TestMethod]
        public async Task GetAccountByAddressReturnsIdenticalInformationBasedOnIndex_LargeIndexValue()
        {
            var manager = new NethereumIndexedWalletManage(2000);
            var wallet = new Wallet<Ethereum.Ethereum>
            {
                Secret = Seed
            };

            var account = await manager.GetAccount(wallet, 1500);
            var foundAccount = await manager.GetAccount(wallet, account.Address);

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
