using SGMOD.Config.Common;

namespace SGMOD.Config;

public class MainConfig {
    //fix
    public bool DisableEnvironmentCheck { get; set; } = true;
    public bool PathRedirection { get; set; } = true;
    public bool DisableIniFileClear { get; set; } = true;
    public bool DisableEncryption { get; set; } = true;
    public bool DisableReboot { get; set; } = true;

    public bool SkipVersionCheck { get; set; } = false;

    //Common
    public bool SinglePlayer { get; set; } = false;
    public DummyLogin DummyLogin { get; set; } = new();
    public CustomCameraID CustomCameraID { get; set; } = new();
}   