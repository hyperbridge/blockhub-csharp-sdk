
public class UpdateSettingsEvent : CodeControl.Message {

    public string profileID;
    public bool chromeExtensionIntegration;
    public bool chromeDataAggregation;
    public bool enableVPN;
    public bool chromeExtensionVerified;
    public bool allowThirdPartyExtensions;
    public string walletSavingDirectory;
    public string extensionSavingDirectory;
}
