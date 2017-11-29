

public class SettingsLoadedEvent : CodeControl.Message
{

    public bool allowThirdPartyExtensions;
    public bool chromeDataAggregation;
    public bool chromeExtensionIntegration;
    public bool enableVPN;
    public string extensionSavingDirectory;
    public string walletSavingDirectory;
    public string profileID;
}
