using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppManager : MonoBehaviour
{
    public static AppManager instance;

    public WalletManager walletManager;
    public ModManager modManager;
    public SaveDataManager saveDataManager;
    public WalletService walletService;
    public ProfileManager profileManager;
    public GameObject screen;
    public GameObject defaultView;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Debug.LogError("More than one AppManager");

        this.walletManager = this.GetComponent<WalletManager>();
        this.modManager = this.GetComponent<ModManager>();
        this.saveDataManager = this.GetComponent<SaveDataManager>();
        this.profileManager = this.GetComponent<ProfileManager>();

        var debugObjects = GameObject.FindGameObjectsWithTag("Debug");

        foreach (GameObject go in debugObjects) {
            GameObject.Destroy(go);
        }
    }

    private void Start() {
        var message = new AppInitializedEvent();
        CodeControl.Message.Send<AppInitializedEvent>(message);
    }

}
