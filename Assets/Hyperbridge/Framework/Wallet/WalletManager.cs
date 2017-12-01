using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Hyperbridge.Core;
using SFB;

namespace Hyperbridge.Wallet
{
    public class WalletManager : MonoBehaviour
    {
        public List<WalletInfo> wallets = new List<WalletInfo>();
        public WalletsView managerView;
        public WalletInfoView infoView;
        string walletPath;
        public string CurrentWalletPath
        {
            get
            {
                return walletPath;
            }
            set
            {
                CodeControl.Message.Send<WalletPathChangedEvent>(new WalletPathChangedEvent { path = value });
                walletPath = value;
            }
        }

        private void Awake()
        {
            CodeControl.Message.AddListener<SettingsLoadedEvent>(this.OnSettingsLoaded);

            CodeControl.Message.AddListener<AppInitializedEvent>(this.OnAppInitialized);
        }
        void OnSettingsLoaded(SettingsLoadedEvent e)
        {
            walletPath = e.loadedSettings.walletSavingDirectory;
            if(walletPath == "")
            {
                walletPath = Application.persistentDataPath + "/" + e.loadedSettings.profileID + "/Wallets";

            }
            this.RefreshWalletList();

        }
        public void OnAppInitialized(AppInitializedEvent e)
        {
        }

        public void RefreshWalletList()
        {
            this.StartCoroutine(this.LoadWallets());
        }

        public void DeleteWallet(WalletInfo wallet)
        {
            this.wallets.Remove(wallet);

            File.Delete(wallet._path);

            this.StartCoroutine(this.LoadWallets());
        }

        public IEnumerator LoadWallets()
        {
            List<WalletInfo> loadedData = new List<WalletInfo>();

           StartCoroutine( Database.LoadAllJSONFilesFromExternalSubFolders<WalletInfo>(CurrentWalletPath,(wallets)=>
            {
                var message = new UpdateWalletsEvent();
                message.wallets = wallets;
                CodeControl.Message.Send<UpdateWalletsEvent>(message);
            }));

           

            yield return wallets;
        }

        public void SelectWalletPath()
        {
            string[] newPath = StandaloneFileBrowser.OpenFolderPanel("Where would you like to save your Wallets?", Application.dataPath, false);

            CurrentWalletPath = newPath[0];



        }
    }
}
