using System.Collections;
using System.Collections.Generic;

public class ProfileData  {

    public string profileName;
    public bool defaultProfile;
    public string imageLocation;
    public int ID;

    public void SetupProfileData(string name, bool isDefault,string imageAddress, int index)
    {
        profileName = name;
        defaultProfile = isDefault;
        imageLocation = imageAddress;
        ID = index;
    }


}
