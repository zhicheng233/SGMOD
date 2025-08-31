using MelonLoader;

[assembly: MelonInfo(typeof(SGMOD.Main), "SGMOD", "1.0.0", "ZCROM", null)]
[assembly: MelonGame("sega-interactive", "Sinmai")]

namespace SGMOD {
    public static partial class ModInfo {
        public const string Name = "SGMOD";
        public const string Author = "ZCROM";
        public const string Version = "1.0.0";
    }

    public class Main : MelonMod {
        public override void OnInitializeMelon() {
            string path = "./" + ModInfo.Name + "/config.json";
            LoggerInstance.Msg("Loading  SGMOD..");
            LoggerInstance.Msg("Loading Config.json..");

            SGMOD.Config.MainConfig config = SGMOD.Config.ConfigManager.Load(path);
        }
    }
}