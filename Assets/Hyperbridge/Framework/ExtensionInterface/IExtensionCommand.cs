using System.Collections;

namespace Hyperbridge.ExtensionInterface
{
    public interface IExtensionCommand
    {
        string Name { get; set; }
        string Value { get; set; }
    }
}