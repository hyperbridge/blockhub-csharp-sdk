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

        public string CurrentWalletPath
        {
            get
            {
                return CurrentWalletPath;
            }
            set
            {
                CodeControl.Message.Send<WalletPathChangedEvent>(new WalletPathChangedEvent { path = value });

            }
        }

        private void Awake()
        {
            CodeControl.Message.AddListener<AppInitializedEvent>(this.OnAppInitialized);
        }

        public void OnAppInitialized(AppInitializedEvent e)
        {
            this.RefreshWalletList();
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
            LoadData loader = LoadData.LoadFromPath("/Resources/Wallets");
            List<WalletInfo> loadedData = new List<WalletInfo>();

            this.wallets = loader.LoadAllFilesFromSubFolder<WalletInfo>();

            var message = new UpdateWalletsEvent();
            message.wallets = wallets;
            CodeControl.Message.Send<UpdateWalletsEvent>(message);

            yield return wallets;
        }

        public void SelectWalletPath()
        {
            string[] newPath = StandaloneFileBrowser.OpenFolderPanel("Where would you like to save your Wallets?", Application.dataPath, false);

            CurrentWalletPath = newPath[0];
        }
    }
}
