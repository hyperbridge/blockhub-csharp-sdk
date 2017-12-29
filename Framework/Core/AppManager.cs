using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Hyperbridge.Networking;
using Hyperbridge.UI;
using Hyperbridge.Wallet;
using Hyperbridge.Profile;
using Hyperbridge.Extension;
using Hyperbridge.OS;

namespace Hyperbridge.Core
{
    public class AppManager : MonoBehaviour
    {
        public static AppManager instance;

        public AppState state;
        public WalletManager walletManager;
        public ExtensionManager extensionManager;
        public SaveDataManager saveDataManager;
        public WalletService walletService;
        public ProfileManager profileManager;
        public GameObject ui;
        public GameObject performanceModal;
        public List<Hyperbridge.UI.Screen> screens;
        public OnlineChecker onlineChecker;


        public void EnableBackgroundMode()
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 30;

            this.performanceModal.SetActive(true);
        }

        public void EnableForegroundMode()
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 30;

            this.performanceModal.SetActive(false);
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            if (hasFocus)
            {
                this.EnableForegroundMode();
            }
            else
            {
                this.EnableBackgroundMode();
            }
        }

        private void OnApplicationPause(bool pauseStatus)
        {
            if (pauseStatus)
            {
                this.EnableBackgroundMode();
            }
            else
            {
                this.EnableForegroundMode();
            }
        }

        private void Awake()
        {
            if (instance == null)
                instance = this;
            else
                // ?? Should this throw an exception instead to stop execution ??
                Debug.LogError("More than one AppManager");

            // TODO: At this point, setup any dependency injection needs

            this.EnableForegroundMode();

            this.state = new AppState();

            var debugObjects = GameObject.FindGameObjectsWithTag("Debug");

            foreach (GameObject go in debugObjects)
            {
                GameObject.Destroy(go);
            }

            var screens = this.ui.GetComponentsInChildren<Hyperbridge.UI.Screen>();

            this.screens.AddRange(screens);

            this.onlineChecker = new OnlineChecker();
            this.StartCoroutine(this.ConnectivityCheck());

            CodeControl.Message.AddListener<NavigateEvent>(this.OnNavigateEvent);

            this.extensionManager.enabled = true;
        }

        private IEnumerator ConnectivityCheck()
        {
            while (true)
            {
                this.StartCoroutine(this.onlineChecker.Check());

                yield return new WaitForSeconds(15.0f);
            }
        }

        private void Start()
        {
            this.StartCoroutine(this.AfterStart());
        }

        public IEnumerator AfterStart()
        {
            yield return new WaitForEndOfFrame();

            CodeControl.Message.Send<AppInitializedEvent>(new AppInitializedEvent());
            CodeControl.Message.Send<NavigateEvent>(new NavigateEvent { path = "/main/home" });
        }

        public void AddScreen(Hyperbridge.UI.Screen screen)
        {
            this.screens.Add(screen);
        }

        public void OnNavigateEvent(NavigateEvent e)
        {
            var path = e.path;

            this.state.uri = path;

            CodeControl.Message.Send<AppStateChangeEvent>(new AppStateChangeEvent { state = this.state });
            CodeControl.Message.Send<MenuEvent>(new MenuEvent { visible = false });
        }

    }
}
