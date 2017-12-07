using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hyperbridge.Extension
{
    // TODO: Why?? <- I'm not too sure either, this is actually dumb. 
    public class ExtensionListManager
    {
        public List<ExtensionInfo> installedExtensions = new List<ExtensionInfo>();
        public List<ExtensionInfo> communityExtensions = new List<ExtensionInfo>();
    }
}