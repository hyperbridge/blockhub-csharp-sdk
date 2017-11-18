using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppManager : MonoBehaviour
{
    public static AppManager instance;

    public AppState state;
    public WalletManager walletManager;
    public ModManager modManager;
    public SaveDataManager saveDataManager;
    public WalletService walletService;
    public ProfileManager profileManager;
    public GameObject ui;
    public List<Screen> screens;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Debug.LogError("More than one AppManager");

        this.state = new AppState();
        //this.walletManager = this.GetComponent<WalletManager>();
        //this.modManager = this.GetComponent<ModManager>();
        //this.saveDataManager = this.GetComponent<SaveDataManager>();
        //this.profileManager = this.GetComponent<ProfileManager>();

        var debugObjects = GameObject.FindGameObjectsWithTag("Debug");

        foreach (GameObject go in debugObjects) {
            GameObject.Destroy(go);
        }

        var screens = this.ui.GetComponentsInChildren<Screen>();

        this.screens.AddRange(screens);

        CodeControl.Message.AddListener<NavigateEvent>(this.OnNavigateEvent);

        this.modManager.enabled = true;
    }

    private void Start() {
        this.StartCoroutine(this.AfterStart());
    }

    public IEnumerator AfterStart() {
        yield return new WaitForSeconds(0.01f);

        CodeControl.Message.Send<AppInitializedEvent>(new AppInitializedEvent());
        CodeControl.Message.Send<NavigateEvent>(new NavigateEvent { path = "/main/home" });

        yield return null;
    }

    public void AddScreen(Screen screen) {
        this.screens.Add(screen);
    }

    public void OnNavigateEvent(NavigateEvent e) {
        var path = e.path;

        this.state.uri = path;

        CodeControl.Message.Send<AppStateChangeEvent>(new AppStateChangeEvent { state = this.state });
        CodeControl.Message.Send<MenuEvent>(new MenuEvent { visible = false });
    }

}
