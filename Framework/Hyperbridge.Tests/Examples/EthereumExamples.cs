using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hyperbridge.Data;
using Hyperbridge.Data.FileSystem;
using Hyperbridge.Ethereum;
using Hyperbridge.Services.Abstract;
using Hyperbridge.Services.Ethereum;
using Hyperbridge.StructureMap;
using Hyperbridge.Wallet;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hyperbridge.Services
{
    [TestClass]
    public class Examples
    {
        private readonly ICoinCurrency Currency = Ether.Instance;

        private string TestDirectory { get; set; }
        private IResolver Resolver { get; set; }
        private IDependencyRoot ApplicationRoot { get; set; }
        private const string WALLET_SECRET = "WALLET SECRET$";

        [TestInitialize]
        public void Initialize()
        {
            TestDirectory = System.IO.Path.GetFullPath("test\\");
            ApplicationRoot = new ApplicationDependencyRoot(TestDirectory);

            var roots = new Dictionary<ICoinCurrency, IDependencyRoot>();
            roots.Add(Ether.Instance, new EthereumDepedencyRoot("ROPSTEN CLIENT URL$", "$ETHERSCAN API KEY$"));
            Resolver = new DependencyRootResolver(roots);
        }

        [TestCleanup]
        public void CleanUp()
        {
            if (System.IO.Directory.Exists(TestDirectory))
            {
                System.IO.Directory.Delete(TestDirectory, true);
            }
        }

        [TestMethod]
        public void GeneratePhraseWithWalletExample()
        {
            var wallet = Resolver.Resolve<IWalletCreator>(Currency).CreateWallet("Test Wallet", "");
            Assert.IsNotNull(wallet.Secret);
            Console.WriteLine($"Generator Seed: {wallet.Secret}");
        }

        [TestMethod, Ignore]
        public void SavingProfileToDiskExample()
        {
            var wallet = Resolver.Resolve<IWalletCreator>(Currency).CreateWallet("SecretTest", "Test Wallet", "");
            
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
                },
                Wallets = new System.Collections.Generic.List<Wallet>
                {
                    wallet
                }
            };

            var filePath = ApplicationRoot.Resolve<ISaver<Profile>>().Save(profile);
            Console.WriteLine($"Path: {filePath}");

            Assert.IsNotNull(filePath);
            Assert.IsTrue(System.IO.File.Exists(new Uri(filePath).AbsolutePath));

            var loadedProfile = ApplicationRoot.Resolve<ILoader<Profile>>().Load(filePath);

            Assert.AreEqual(profile.Id, loadedProfile.Id);
            Assert.AreEqual(profile.ImageUri, loadedProfile.ImageUri);
            Assert.AreEqual(profile.Name, loadedProfile.Name);
            Assert.AreEqual(profile.Notifications.Count, loadedProfile.Notifications.Count);
            Assert.AreEqual(profile.Wallets.Count, loadedProfile.Wallets.Count);

            Assert.AreEqual(profile.Notifications[0].HasBeenShown, loadedProfile.Notifications[0].HasBeenShown);
            Assert.AreEqual(profile.Notifications[0].Subject, loadedProfile.Notifications[0].Subject);
            Assert.AreEqual(profile.Notifications[0].Text, loadedProfile.Notifications[0].Text);
            Assert.AreEqual(profile.Notifications[0].TimeStamp, loadedProfile.Notifications[0].TimeStamp);
            Assert.AreEqual(profile.Notifications[0].Type, loadedProfile.Notifications[0].Type);

            Assert.AreEqual(profile.Wallets[0].BlockchainType, loadedProfile.Wallets[0].BlockchainType);
            Assert.AreEqual(profile.Wallets[0].Id, loadedProfile.Wallets[0].Id);
            Assert.AreEqual(profile.Wallets[0].LastIndexUsed, loadedProfile.Wallets[0].LastIndexUsed);
            Assert.AreEqual(profile.Wallets[0].Name, loadedProfile.Wallets[0].Name);
            Assert.AreEqual(profile.Wallets[0].Secret, loadedProfile.Wallets[0].Secret);
            Assert.AreEqual(profile.Wallets[0].Accounts.Count, loadedProfile.Wallets[0].Accounts.Count);
        }

        [TestMethod, Ignore]
        public async Task GetBalanceExample()
        {
            var wallet = Resolver.Resolve<IWalletCreator>(Currency).CreateWallet(WALLET_SECRET, "Test Wallet", "");
            var account = Resolver.Resolve<IAccountCreator>(Currency).CreateAccount(wallet, "Account 1");
            var balance = await Resolver.Resolve<IAccountBalanceReader>(Currency).GetAccountBalance(account);

            Console.WriteLine($"Balance: {balance}");
        }

        [TestMethod, Ignore]
        public async Task SendTransactionExample()
        {
            var wallet = Resolver.Resolve<IWalletCreator>(Currency).CreateWallet(WALLET_SECRET, "Test Wallet", "");
            var account1 = Resolver.Resolve<IAccountCreator>(Currency).CreateAccount(wallet, "Account 1");
            var account2 = Resolver.Resolve<IAccountCreator>(Currency).CreateAccount(wallet, "Account 2");

            Assert.AreNotEqual(account1.Address, account2.Address, true);

            var transactionHash = await Resolver.Resolve<ITransactionWrite>(Currency).SendTransaction(new SendTransaction
            {
                Amount = 100M,
                Unit = "WEI",
                Currency = Ether.Instance,
                FromAddress = account1,
                ToAddress = account2.Address,
                TimeStamp = DateTime.Now
            });

            Console.WriteLine($"From Address: {account1.Address}");
            Console.WriteLine($"To Address: {account2.Address}");
            Console.WriteLine($"Transaction Hash: {transactionHash}");
        }

        [TestMethod, Ignore]
        public async Task ReadLast25TransactionsExample()
        {
            var wallet = Resolver.Resolve<IWalletCreator>(Currency).CreateWallet(WALLET_SECRET, "Test Wallet", "");
            var account = Resolver.Resolve<IAccountCreator>(Currency).CreateAccount(wallet, "Account 1");

            var transactions = await Resolver.Resolve<ITransactionRead>(Currency).GetLastTransactions(account);

            Console.WriteLine($"Total Transactions: {transactions.Count()}");
            foreach(var t in transactions)
            {
                Console.WriteLine($"TimeStamp: {t.TimeStamp}, Amount: {t.Amount} {t.Unit}, From: {t.FromAddress}, To: {t.ToAddress}");
            }
        }

        [TestMethod, Ignore]
        public async Task ReadLast25SentTransactionsExample()
        {
            var wallet = Resolver.Resolve<IWalletCreator>(Currency).CreateWallet(WALLET_SECRET, "Test Wallet", "");
            var account = Resolver.Resolve<IAccountCreator>(Currency).CreateAccount(wallet, "Account 1");

            var transactions = await Resolver.Resolve<ITransactionRead>(Currency).GetLastSentTransactions(account);

            Console.WriteLine($"Total Transactions: {transactions.Count()}");
            foreach (var t in transactions)
            {
                Console.WriteLine($"TimeStamp: {t.TimeStamp}, Amount: {t.Amount} {t.Unit}, From: {t.FromAddress.Address}, To: {t.ToAddress}");
            }
        }

        [TestMethod, Ignore]
        public async Task ReadLast25ReceivedTransactionsExample()
        {
            var wallet = Resolver.Resolve<IWalletCreator>(Currency).CreateWallet(WALLET_SECRET, "Test Wallet", "");
            var account = Resolver.Resolve<IAccountCreator>(Currency).CreateAccount(wallet, "Account 1");

            var transactions = await Resolver.Resolve<ITransactionRead>(Currency).GetLastReceivedTransactions(account);

            Console.WriteLine($"Total Transactions: {transactions.Count()}");
            foreach (var t in transactions)
            {
                Console.WriteLine($"TimeStamp: {t.TimeStamp}, Amount: {t.Amount} {t.Unit}, From: {t.FromAddress}, To: {t.ToAddress.Address}");
            }
        }

        [TestMethod, Ignore]
        public async Task ReadLast25ReceivedTransactions_Page2_Example()
        {
            var wallet = Resolver.Resolve<IWalletCreator>(Currency).CreateWallet(WALLET_SECRET, "Test Wallet", "");
            var account = Resolver.Resolve<IAccountCreator>(Currency).CreateAccount(wallet, "Account 1");

            var transactions = await Resolver.Resolve<ITransactionRead>(Currency).GetLastReceivedTransactions(account, 2);

            Console.WriteLine($"Total Transactions: {transactions.Count()}");
            foreach (var t in transactions)
            {
                Console.WriteLine($"TimeStamp: {t.TimeStamp}, Amount: {t.Amount} {t.Unit}, From: {t.FromAddress}, To: {t.ToAddress.Address}");
            }
        }
    }
}
