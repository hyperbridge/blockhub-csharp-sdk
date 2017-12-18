using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Hyperbridge.Core;
using SFB;
using Hyperbridge.Profile;
using System;

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
            CodeControl.Message.AddListener<EditWalletEvent>(this.OnWalletEdited);

        }

        private void OnWalletEdited(EditWalletEvent e)
        {

            string previousWalletName = e.wallet.title;
            WalletInfo editedWallet = new WalletInfo { title = e.editedName, coinType = e.wallet.coinType, uuid = e.wallet.uuid, privateKey = e.wallet.privateKey, secret = e.wallet.secret , address = e.wallet.address , info = e.wallet.info } ;
            editedWallet._path = AppManager.instance.walletManager.CurrentWalletPath + "/" + editedWallet.uuid + "/" + editedWallet.title +".json";
            wallets.Add(editedWallet);
            Database.SaveJSONToExternal<WalletInfo>(AppManager.instance.walletManager.CurrentWalletPath + "/" + editedWallet.uuid, editedWallet.title, editedWallet);
            DeleteWallet(e.wallet, RefreshWalletList);
            this.RefreshWalletList();

        }

        private void OnSettingsLoaded(SettingsLoadedEvent e)
        {
            if (walletPath == e.loadedSettings.walletSavingDirectory) return;

            walletPath = e.loadedSettings.walletSavingDirectory;
            if (walletPath == "")
            {
                walletPath = Application.persistentDataPath + "/" + e.loadedSettings.profileID + "/Wallets/";

            }
            this.RefreshWalletList();

        }

        public void RefreshWalletList()
        {
            this.StartCoroutine(this.LoadWallets());
        }

        public void DeleteWallet(WalletInfo wallet, Action callback)
        {
            this.wallets.Remove(wallet);
            File.Delete(wallet._path);

            callback();
        }

        public IEnumerator LoadWallets()
        {
            Debug.Log("[WalletManager] Loading wallets... Path: " + CurrentWalletPath);
            List<WalletInfo> loadedData = new List<WalletInfo>();
            if (!Directory.Exists(CurrentWalletPath))
            {
                /*  ErrorEvent message = new ErrorEvent { errorMessage = "No wallets Found", errorDate = System.DateTime.Now.Year + " " + System.DateTime.Now.Month + " " + System.DateTime.Now.Day, errorType = "Folder Not Found" };
                  CodeControl.Message.Send<ErrorEvent>(message);
                  CodeControl.Message.Send<UpdateWalletsEvent>(null);
                  */
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

        WalletInfo FindWalletByID(string uuid)
        {
            foreach (WalletInfo w in this.wallets)
            {
                if (name == w.uuid)
                {
                    return w;
                }
            }

            return null;
        }
    }
}
