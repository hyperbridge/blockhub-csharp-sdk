using Blockhub;
using Blockhub.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Zenject;

public class CreateProfileButton : BaseButton {
    [Inject]
    public ISave<Profile> ProfileSaver { get; set; }

    [Inject]
    public IProfileContextAccess ProfileAccessor { get; set; }

    protected override async void OnButtonClick()
    {
        var profile = new Profile
        {
            Id = Guid.NewGuid().ToString(),
            Name = "General Profile"
        };

        var uri = await ProfileSaver.Save(profile);
        Assert.AreEqual(uri, profile.ProfileUri, "Profile Uri not properly set.", StringComparer.OrdinalIgnoreCase);

        Debug.Log($"File Uri: {uri}");
        Debug.Log($"File Uri: {new Uri(uri).LocalPath}");
        var exists = System.IO.File.Exists(new Uri(uri).LocalPath);
        Assert.IsTrue(exists, "File was not properly saved.");

        ProfileAccessor.Profile = profile;

        if (exists) Message($"Created profile file at uri '{uri}' successfully!");
    }
}
