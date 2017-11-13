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

        walletManager = GetComponent<WalletManager>();
        modManager = GetComponent<ModManager>();
        saveDataManager = GetComponent<SaveDataManager>();
        profileManager = GetComponent<ProfileManager>();

        //Wicked Smart.
        var debugObjects = GameObject.FindGameObjectsWithTag("Debug");

        foreach (GameObject go in debugObjects)
        {
            Destroy(go);
        }
    }

}
