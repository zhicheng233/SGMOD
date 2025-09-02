using System.Runtime.InteropServices;
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
        //导入dll以设置控制台编码
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool SetConsoleOutputCP(uint wCodePageID);

        public override void OnInitializeMelon() {
            //设置控制台编码为UTF-8
            SetConsoleOutputCP(65001);
            PrintLogo();
            PrintWarng();
            string path = "./" + ModInfo.Name + "/config.json";
            MelonLogger.Msg("Loading  SGMOD..");
            MelonLogger.Msg("Loading Config.json..");

            SGMOD.Config.MainConfig config = SGMOD.Config.ConfigManager.Load(path);

            if (config.DummyLogin.Enabled == true && config.CustomCameraID.Enabled == true) {
                MelonLogger.Warning("DummyLogin has been enable,disable CustomCameraID");
                config.CustomCameraID.Enabled = false;
            }

            if (config.CustomCameraID.Enabled == true) {
                Core.Common.CustomCameraID.PrintCameraIDList();
            }

            if (config.DisableEnvironmentCheck) PatchIt(typeof(SGMOD.Core.Fix.DisableEnvironmenntCheck));
            if (config.PathRedirection) PatchIt(typeof(SGMOD.Core.Fix.PathRedirection));
        }


        public static void PatchIt(Type cs) {
            try {
                MelonLogger.Msg("Patching " + cs.Name + "...");
                HarmonyLib.Harmony.CreateAndPatchAll(cs);
            }
            catch (Exception e) {
                MelonLogger.Error("Failed to patch:" + cs.Name);
                MelonLogger.Error("Message:\n" + e.Message);
                MelonLogger.Error("InnerException:\n" + e.InnerException);
            }
        }

        public static void PrintLogo() {
            MelonLogger.Msg(
                "\n .d8888b.   .d8888b.  888b     d888  .d88888b.  8888888b.  \nd88P  Y88b d88P  Y88b 8888b   d8888 d88P\" \"Y88b 888  \"Y88b \nY88b.      888    888 88888b.d88888 888     888 888    888 \n \"Y888b.   888        888Y88888P888 888     888 888    888 \n    \"Y88b. 888  88888 888 Y888P 888 888     888 888    888 \n      \"888 888    888 888  Y8P  888 888     888 888    888 \nY88b  d88P Y88b  d88P 888   \"   888 Y88b. .d88P 888  .d88P \n \"Y8888P\"   \"Y8888P88 888       888  \"Y88888P\"  8888888P\"  \n                                                           \n                                                           ");
        }

        public static void PrintWarng() {
            MelonLogger.Warning(
                "\n⚠ 该MOD包含作弊功能，意味着你已经了解该使用该MOD的风险\n" +
                "⚠ This mod contains cheating features, which means you are aware of the risks of using it."
            );
        }
    }
}