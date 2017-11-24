using System.Collections;
using System.Collections.Generic;

public interface IExtension {
    List<IExtensionCommand> GetOutgoingCommands();

    void AddIncomingCommands(List<IExtensionCommand> commands);

    void AddOutgoingCommands(List<IExtensionCommand> commands);

    void ClearOutgoingCommands();

    void ClearIncomingCommands();

    void ParseCommand(IExtensionCommand command);

    void Update();
}
