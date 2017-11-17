using System.Collections;
using System.Collections.Generic;

public class ProfileData
{
    public string name;
    public bool isDefault;
    public string imageLocation;
    public string uuid;

    public void SetupProfileData(string name, bool isDefault, string imageLocation, string uuid)
    {
        this.name = name;
        this.isDefault = isDefault;
        this.imageLocation = imageLocation;
        this.uuid = uuid;
    }
}
