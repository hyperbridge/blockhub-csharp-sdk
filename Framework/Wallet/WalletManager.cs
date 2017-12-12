using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Hyperbridge.Core;
using SFB;
using Hyperbridge.Profile;

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

        }

        private void OnSettingsLoaded(SettingsLoadedEvent e)
        {
            if (walletPath == e.loadedSettings.walletSavingDirectory) return;

            walletPath = e.loadedSettings.walletSavingDirectory;
            if (walletPath == "")
            {
                walletPath = Application.persistentDataPath + "/" + e.loadedSettings.profileID + "/Wallets";

            }
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
            Debug.Log("[WalletManager] Loading wallets... Path: " + CurrentWalletPath);
            List<WalletInfo> loadedData = new List<WalletInfo>();
            if (!Directory.Exists(CurrentWalletPath))
            {
                CodeControl.Message.Send<UpdateWalletsEvent>(null);
                yield break;

            }

            StartCoroutine(Database.LoadAllJSONFilesFromExternalSubFolders<WalletInfo>(CurrentWalletPath, (wallets) =>
            {
                CodeControl.Message.Send<UpdateWalletsEvent>(new UpdateWalletsEvent { wallets = wallets });
            }));

            yield return wallets;
        }

        public void SelectWalletPath()
        {
            string[] newPath = StandaloneFileBrowser.OpenFolderPanel("Save Wallets At:", Application.dataPath, false);

            CurrentWalletPath = newPath[0];



        }
    }
}
