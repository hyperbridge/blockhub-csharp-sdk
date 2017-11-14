using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SuperScrollView;
using Devdog.General.UI;

public class ManageProfilesView : MonoBehaviour
{

    public LoopListView2 myLoopList;
    ProfileManager _profileManager;
    public UIWindowPage editProfileView, deleteProfileConfirmationView;
    private void Awake()
    {

    }
    private void Start()
    {
        _profileManager = AppManager.instance.profileManager;
        AppManager.instance.profileManager._manageProfilesView = this;
        StartCoroutine(SetupListView());

    }
    void OnEnable()
    {


    }


    public void ShowEditProfileView(ProfileData dataToEdit)
    {
        editProfileView.Show();
        editProfileView.GetComponent<EditProfileView>().StartEditingProfile(dataToEdit);
    }


    public void ShowDeleteProfileConfirmationView()
    {
        deleteProfileConfirmationView.Show();
    }

    //This is required for UI OnShow to access the coroutine.
    public void StartSetupList()
    {
        StartCoroutine(SetupListView());
    }
    public void ResetListItem()
    {
        myLoopList.SetListItemCount(0, true);
       

        StartCoroutine(SetupListView());
        

    }
    public IEnumerator SetupListView()
    {
        yield return _profileManager.LoadExistingProfiles();
        Debug.Log(_profileManager.LoadExistingProfiles().Count);
        yield return new WaitForEndOfFrame();
        myLoopList.InitListView(_profileManager.existingProfiles.Count, OnGetItemByIndex);

    }
    public IEnumerator ResetListView()
    {
        myLoopList.RefreshAllShownItem();
        yield return null;
    }
    LoopListViewItem2 OnGetItemByIndex(LoopListView2 listView, int index)
    {
        //Steps: We need to know the total amount of items we're spawning. 
        //Then the data for each item.
        // Then, we need to assign data to the items. This data is: Username, User picture and a Json file maybe with the config?
        // int ID = 0;
        if (index < 0 || index >= _profileManager.existingProfiles.Count)
        {
            return null;
        }

        ProfileData profileData = _profileManager.GetProfileDataByIndex(index);
        if (profileData == null)
        {
            return null;
        }
        LoopListViewItem2 item = listView.NewListViewItem("ProfileContainer");
        ProfileContainer profileContainerController = item.GetComponent<ProfileContainer>();
        


        profileContainerController.SetupProfile(null, profileData.profileName, index,profileData);


        return item;

    }
}
