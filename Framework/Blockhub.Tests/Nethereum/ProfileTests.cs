using System;
using System.Linq;
using System.Threading.Tasks;
using Blockhub.Tests;
using Blockhub.Wallet;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Blockhub.Nethereum
{
    [TestClass]
    public class ProfileTests
    {
        private IWalletCreate<T> NewWalletCreator<T>(string walletName) where T : IBlockchainType
        {
            var wallet = new Wallet<T>
            {
                Name = walletName
            };

            return new EmulateWalletCreate<T>(wallet);
        }

        private IWalletCreate<T> NewWalletCreator<T>(Wallet<T> wallet) where T : IBlockchainType
        {
            return new EmulateWalletCreate<T>(wallet);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void LoadProfileContextLoad_NullInnerLoad_ThrowsArgumentNullException()
        {
            var load = new LoadProfileContextLoad(null, new ProfileContextAccess());
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void LoadProfileContextLoad_NullAccessor_ThrowsArgumentNullException()
        {
            var load = new LoadProfileContextLoad(new EmulateLoad<Profile>(uri => null), null);
        }

        [TestMethod]
        public async Task LoadProfileContextLoad_Load_AccessorSet()
        {
            var profile = new Profile();
            var loader = new EmulateLoad<Profile>(uri => profile);
            var accessor = new ProfileContextAccess();

            // Pre-Conditions
            Assert.IsNull(accessor.Profile);
            
            var contextLoader = new LoadProfileContextLoad(loader, accessor);
            var loadedProfile = await contextLoader.Load("");

            // Post-Conditions
            Assert.IsNotNull(accessor.Profile);
            Assert.AreEqual(profile, loadedProfile);
            Assert.AreEqual(profile, accessor.Profile);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void AutoAddProfileWalletCreate_NullInnerCreate_ThrowsArgumentNullException()
        {
            var creator = new AutoAddToProfileWalletCreate<Ethereum.Ethereum>(null, new ProfileContextAccess());
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void AutoAddProfileWalletCreate_NullAccessor_ThrowsArgumentNullException()
        {
            var creator = new AutoAddToProfileWalletCreate<Ethereum.Ethereum>(new EmulateWalletCreate<Ethereum.Ethereum>(null), null);
        }

        [TestMethod, ExpectedException(typeof(ProfileNotSetException))]
        public async Task AutoAddProfileWalletCreate_ProfileNotSetException()
        {
            // Setup
            var wallet = new Wallet<Ethereum.Ethereum>();
            var walletCreator = new EmulateWalletCreate<Ethereum.Ethereum>(wallet);
            var accessor = new ProfileContextAccess();

            var creator = new AutoAddToProfileWalletCreate<Ethereum.Ethereum>(walletCreator, accessor);
            await creator.CreateWallet("", "");
        }

        [TestMethod, ExpectedException(typeof(WalletAlreadyExistsInProfileException))]
        public async Task AutoAddProfileWalletCreate_WalletExists_ThrowsWalletAlreadyExistsInProfileException()
        {
            // Setup
            var wallet = new Wallet<Ethereum.Ethereum>
            {
                Name = ""
            };
            var walletCreator = NewWalletCreator(wallet);
            
            var accessor = new ProfileContextAccess();
            accessor.Profile = new Profile();
            accessor.Profile.ProfileObjects.Add(wallet);

            var creator = new AutoAddToProfileWalletCreate<Ethereum.Ethereum>(walletCreator, accessor);
            var outputWallet = await creator.CreateWallet("", "");
        }

        [TestMethod]
        public async Task AutoAddProfileWalletCreate_WalletAddedToProfile()
        {
            // Setup
            var wallet = new Wallet<Ethereum.Ethereum>();
            var walletCreator = new EmulateWalletCreate<Ethereum.Ethereum>(wallet);
            var accessor = new ProfileContextAccess();
            accessor.Profile = new Profile();

            var creator = new AutoAddToProfileWalletCreate<Ethereum.Ethereum>(walletCreator, accessor);
            var outputWallet = await creator.CreateWallet("", "");

            // Assert
            Assert.AreEqual(wallet, outputWallet);

            var profile = accessor.Profile;
            var profileWallets = profile.GetWallets<Ethereum.Ethereum>();
            Assert.AreEqual(1, profileWallets.Count());
            Assert.AreEqual(wallet, profileWallets.First());
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void AutoSaveProfileWalletCreate_NullInnerCreate_ThrowsArgumetNullException()
        {
            var creator = new AutoSaveProfileWalletCreate<Ethereum.Ethereum>(null, new ProfileContextAccess(), new EmulateSave<Profile>());
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void AutoSaveProfileWalletCreate_NullAccessor_ThrowsArgumentNullException()
        {
            var innerCreate = new EmulateWalletCreate<Ethereum.Ethereum>(null);
            var creator = new AutoSaveProfileWalletCreate<Ethereum.Ethereum>(innerCreate, null, new EmulateSave<Profile>());
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void AutoSaveProfileWalletCreate_NullSaver_ThrowsArgumentNullException()
        {
            var innerCreate = new EmulateWalletCreate<Ethereum.Ethereum>(null);
            var creator = new AutoSaveProfileWalletCreate<Ethereum.Ethereum>(innerCreate, new ProfileContextAccess(), null);
        }

        [TestMethod, ExpectedException(typeof(ProfileNotSetException))]
        public async Task AutoSaveProfileWalletCreate_ProfileNotSet_ThrowsProfileNotSetException()
        {
            var innerCreate = new EmulateWalletCreate<Ethereum.Ethereum>(null);
            var creator = new AutoSaveProfileWalletCreate<Ethereum.Ethereum>(innerCreate, new ProfileContextAccess(), new EmulateSave<Profile>());

            var wallet = await creator.CreateWallet("", "");
        }

        [TestMethod]
        public async Task AutoSaveProfileWalletCreate_Saved()
        {
            var wallet = new Wallet<Ethereum.Ethereum>();
            var innerCreate = new EmulateWalletCreate<Ethereum.Ethereum>(wallet);
            var saver = new EmulateSave<Profile>();
            var accessor = new ProfileContextAccess();
            accessor.Profile = new Profile();

            var creator = new AutoSaveProfileWalletCreate<Ethereum.Ethereum>(innerCreate, accessor, saver);

            var finalWallet = await creator.CreateWallet("", "");
            Assert.AreEqual(wallet, finalWallet);
            Assert.IsTrue(saver.SaveCalled);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void AutoSaveProfileAccountCreate_NullInnerCreate_ThrowsArgumentNullException()
        {
            var creator = new AutoSaveProfileAccountCreate<Ethereum.Ethereum>(null, new ProfileContextAccess(), new EmulateSave<Profile>());
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void AutoSaveProfileAccountCreate_NullAccessor_ThrowsArgumentNullException()
        {
            var innerCreate = new EmulateAccountCreate<Ethereum.Ethereum>(null);
            var creator = new AutoSaveProfileAccountCreate<Ethereum.Ethereum>(innerCreate, null, new EmulateSave<Profile>());
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void AutoSaveProfileAccountCreate_NullSaver_ThrowsArgumentNullException()
        {
            var innerCreate = new EmulateAccountCreate<Ethereum.Ethereum>(null);
            var creator = new AutoSaveProfileAccountCreate<Ethereum.Ethereum>(innerCreate, new ProfileContextAccess(), null);
        }

        [TestMethod, ExpectedException(typeof(ProfileNotSetException))]
        public async Task AutoSaveProfileAccountCreate_ProfileNotSet_ThrowsProfileNotSetException()
        {
            var innerCreate = new EmulateAccountCreate<Ethereum.Ethereum>(null);
            var creator = new AutoSaveProfileAccountCreate<Ethereum.Ethereum>(innerCreate, new ProfileContextAccess(), new EmulateSave<Profile>());

            await creator.CreateAccount(null, 0);
        }

        [TestMethod]
        public async Task AutoSaveProfileAccountCreate_ProfileSaved()
        {
            var account = new Account<Ethereum.Ethereum>();
            var innerCreate = new EmulateAccountCreate<Ethereum.Ethereum>(account);
            var accessor = new ProfileContextAccess();
            accessor.Profile = new Profile();

            var saver = new EmulateSave<Profile>();
            var creator = new AutoSaveProfileAccountCreate<Ethereum.Ethereum>(innerCreate, accessor, saver);

            var returnedAccount = await creator.CreateAccount(null, 0);
            Assert.AreEqual(account, returnedAccount);
            Assert.IsTrue(saver.SaveCalled);
        }
    }
}
