using System.Collections;
using System.Collections.Generic;

namespace Hyperbridge.ExtensionInterface
{
    public interface IExtensionBridge
    {
        List<IExtensionCommand> GetOutgoingCommands();

        void AddIncomingCommands(List<IExtensionCommand> commands);

        void AddOutgoingCommands(List<IExtensionCommand> commands);

        void ClearOutgoingCommands();

        void ClearIncomingCommands();

        void ParseCommand(IExtensionCommand command);

        void Update();
    }
}
