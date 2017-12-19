using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hyperbridge.Profile
{
    public class ApplicationSettingsUpdatedEvent : CodeControl.Message
    {
        public ApplicationSettings applicationSettings = new ApplicationSettings();
        public bool firstLoad;
    }

}
