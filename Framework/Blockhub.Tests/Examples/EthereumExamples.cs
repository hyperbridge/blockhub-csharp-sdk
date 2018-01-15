using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blockhub.Data;
using Blockhub.Ethereum;
using Blockhub.Nethereum;
using Blockhub.Transaction;
using Blockhub.Wallet;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Blockhub.Services
{
    [TestClass]
    public class Examples
    {
        private readonly ITokenSource Currency = Blockhub.Ethereum.Ethereum.Instance;

        private string ProfileDirectory { get; set; }
        private const string WALLET_SECRET = "";
        private const string CLIENT_URL = "";
        private const string ETHER_SCAN_API_KEY = "";
        [TestInitialize]
        public void Initialize()
        {
            ProfileDirectory = System.IO.Path.GetFullPath("test\\");
        }

        [TestCleanup]
        public void CleanUp()
        {
            if (System.IO.Directory.Exists(ProfileDirectory))
            {
                System.IO.Directory.Delete(ProfileDirectory, true);
            }
        }

        #region Helpers
        private Ethereum.EthereumWalletCreate WalletCreator()
        {
            return new Ethereum.EthereumWalletCreate(new Bip39SeedGenerate(NBitcoin.Wordlist.English, NBitcoin.WordCount.Twelve));
        }

        private NethereumAccountCreate WalletManager()
        {
            return new NethereumAccountCreate();
        }

        private FileSystemProfileSave ProfileSaver()
        {
            return new FileSystemProfileSave(ProfileDirectory);
        }

        private FileSystemJsonLoad<T> Loader<T>()
        {
            return new FileSystemJsonLoad<T>();
        }

        private NethereumBalanceRead AccountBalance()
        {
            return new NethereumBalanceRead(CLIENT_URL);
        }

        private NethereumTransactionWrite TransactionWrite()
        {
            return new NethereumTransactionWrite(CLIENT_URL);
        }

        private EtherScan.EtherScanLastTransactionRead TransactionRead()
        {
            return new EtherScan.EtherScanLastTransactionRead(ETHER_SCAN_API_KEY);
        }
        #endregion

        [TestMethod]
        public async Task GeneratePhraseWithWalletExample()
        {
            var wallet = await WalletCreator().CreateWallet("Test Wallet", "");
            Assert.IsNotNull(wallet.Secret);
            Console.WriteLine($"Generator Seed: {wallet.Secret}");
        }

        [TestMethod, Ignore]
        public async Task SavingProfileToDiskExample()
        {
            var wallet = await WalletCreator().CreateWallet("candy maple cake bread pudding cream honey grace smooth crumble sweet blanket", "Test Wallet", "");
            var account = await WalletManager().CreateAccount(wallet, 0);
            wallet.Accounts.Add(account);

            var notification = new Notification
            {
                HasBeenShown = false,
                Subject = "Test Notification",
                Text = "This is simply a test notification",
                TimeStamp = DateTime.UtcNow,
                Type = ""
            };

            var profile = new Profile
            {
                Id = Guid.NewGuid().ToString(),
                ImageUri = "",
                Name = "Test Profile",
                Notifications = new System.Collections.Generic.List<Notification>
                {
                    notification
                }
            };

            profile.ProfileObjects.Add(wallet);

            var filePath = await ProfileSaver().Save(profile);
            Console.WriteLine($"Path: {filePath}");

            Assert.IsNotNull(filePath);
            Assert.IsTrue(System.IO.File.Exists(new Uri(filePath).AbsolutePath));

            var loadedProfile = await Loader<Profile>().Load(filePath);

            Assert.AreEqual(profile.Id, loadedProfile.Id);
            Assert.AreEqual(profile.ImageUri, loadedProfile.ImageUri);
            Assert.AreEqual(profile.Name, loadedProfile.Name);
            Assert.AreEqual(profile.Notifications.Count, loadedProfile.Notifications.Count);

            var expectedWallets = profile.GetWallets<Ethereum.Ethereum>().ToArray();
            var loadedWallets = loadedProfile.GetWallets<Ethereum.Ethereum>().ToArray();
            Assert.AreEqual(expectedWallets.Count(), loadedWallets.Count());
            Assert.IsTrue(expectedWallets.Count() > 0);
            Assert.IsTrue(loadedWallets.Count() > 0);

            Assert.AreEqual(profile.Notifications[0].HasBeenShown, loadedProfile.Notifications[0].HasBeenShown);
            Assert.AreEqual(profile.Notifications[0].Subject, loadedProfile.Notifications[0].Subject);
            Assert.AreEqual(profile.Notifications[0].Text, loadedProfile.Notifications[0].Text);
            Assert.AreEqual(profile.Notifications[0].TimeStamp, loadedProfile.Notifications[0].TimeStamp);
            Assert.AreEqual(profile.Notifications[0].Type, loadedProfile.Notifications[0].Type);

            Assert.AreEqual(expectedWallets[0].Id, loadedWallets[0].Id);
            Assert.AreEqual(expectedWallets[0].Name, loadedWallets[0].Name);
            Assert.AreEqual(expectedWallets[0].Secret, loadedWallets[0].Secret);

            var expectedAccounts = expectedWallets[0].Accounts.ToArray();
            var loadedAccounts = loadedWallets[0].Accounts.ToArray();

            Assert.AreEqual(expectedAccounts.Count(), loadedAccounts.Count());
            Assert.IsTrue(expectedAccounts.Count() > 0);

            Assert.AreEqual(expectedAccounts[0].Address, loadedAccounts[0].Address);
            Assert.AreEqual(expectedAccounts[0].Id, loadedAccounts[0].Id);
            Assert.AreEqual(expectedAccounts[0].Name, loadedAccounts[0].Name);
            Assert.IsNull(loadedAccounts[0].GetPrivateKey());
        }

        [TestMethod, Ignore]
        public async Task GetBalanceExample()
        {
            Wallet<Ethereum.Ethereum> wallet = await WalletCreator().CreateWallet(WALLET_SECRET, "Test Wallet", "");
            var account = await WalletManager().CreateAccount(wallet, 0);
            var balance = await AccountBalance().GetBalance(account);

            Console.WriteLine($"Balance: {balance}");
        }

        [TestMethod, Ignore]
        public async Task SendTransactionExample()
        {
            var wallet = await WalletCreator().CreateWallet(WALLET_SECRET, "Test Wallet", "");
            var account1 = await WalletManager().CreateAccount(wallet, 0);
            var account2 = await WalletManager().CreateAccount(wallet, 1);

            // To test the private key loader
            account1.SetPrivateKey(null);

            Assert.AreNotEqual(account1.Address, account2.Address, true);

            var writer = new LoadMissingPrivateKeyTransactionWrite<Ethereum.Ethereum>(TransactionWrite(), new NethereumPrivateKeyGenerate());
            var response = await writer.SendTransactionAsync(account1, account2.Address, new WeiCoin(new NBitcoin.BouncyCastle.Math.BigInteger("100")));

            Console.WriteLine($"From Address: {account1.Address}");
            Console.WriteLine($"To Address: {account2.Address}");
            Console.WriteLine($"Transaction Hash: {response.TransactionHash}");
        }

        [TestMethod, Ignore]
        public async Task ReadLast25TransactionsExample()
        {
            var wallet = await WalletCreator().CreateWallet(WALLET_SECRET, "Test Wallet", "");
            var account = await WalletManager().CreateAccount(wallet, 0);

            var transactions = await TransactionRead().GetLastTransactions(account.Address, 1, 25);

            Console.WriteLine($"Total Transactions: {transactions.Count()}");
            foreach(var t in transactions)
            {
                Console.WriteLine($"TimeStamp: {t.TransactionTime}, Amount: {t.GetAmount()} WEI, From: {t.FromAddress}, To: {t.ToAddress}");
            }
        }

        [TestMethod, Ignore]
        public async Task ReadLast25Transactions_Page2_Example()
        {
            var wallet = await WalletCreator().CreateWallet(WALLET_SECRET, "Test Wallet", "");
            var account = await WalletManager().CreateAccount(wallet, 0);

            var transactions = await TransactionRead().GetLastTransactions(account.Address, 2, 25);

            Console.WriteLine($"Total Transactions: {transactions.Count()}");
            foreach (var t in transactions)
            {
                Console.WriteLine($"TimeStamp: {t.TransactionTime}, Amount: {t.GetAmount()} WEI, From: {t.FromAddress}, To: {t.ToAddress}");
            }
        }
    }
}
