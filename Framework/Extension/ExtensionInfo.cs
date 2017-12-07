using System.Collections.Generic;
using Hyperbridge.Extension;
using UMod;

namespace Hyperbridge.Extension
{
    public class ExtensionInfo
    {
        public string descriptionText, tags, name, updateDate, path, imagePath, version, uuid;
        public int installs;
        public float rating;
        public bool enabled;
        public List<Capability> capabilities = new List<Capability>();
        public ModHost mod;
    }
}