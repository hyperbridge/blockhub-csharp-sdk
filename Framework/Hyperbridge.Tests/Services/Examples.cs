using System;
using Hyperbridge.Data.FileSystem;
using Hyperbridge.Ethereum;
using Hyperbridge.Services.Abstract;
using Hyperbridge.Wallet;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hyperbridge.Services
{
    [TestClass]
    public class Examples
    {
        const string TEST_PATH = "test\\";

        [TestCleanup]
        public void CleanUp()
        {
            var path = System.IO.Path.GetFullPath(TEST_PATH);
            if (System.IO.Directory.Exists(path))
            {
                System.IO.Directory.Delete(path, true);
            }
        }

        [TestMethod]
        public void SavingProfileToDisk()
        {
            var wallet = new InMemoryWalletCreator(Ether.Instance).CreateWallet("SecretTest", "Test Wallet", "");
            
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

            var path = System.IO.Path.GetFullPath(TEST_PATH);

            var saver = new FileSystemProfileSaver(path);
            var filePath = saver.Save(profile);
            Console.WriteLine($"Path: {filePath}");

            Assert.IsNotNull(filePath);
            Assert.IsTrue(System.IO.File.Exists(new Uri(filePath).AbsolutePath));

            var loader = new FileSystemLoader<Profile>();
            var loadedProfile = loader.Load(filePath);

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
    }
}
