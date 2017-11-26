using Hyperbridge.ExtensionInterface;

namespace Hyperbridge.Extension
{
    public class ExtensionCommand : IExtensionCommand
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}