using HarmonyLib;

namespace SGMOD.Core.Fix;

public class DisableIniFileClear {
    [HarmonyPrefix]
    [HarmonyPatch(typeof(MAI2System.IniFile), "clear")]
    private static bool Clear()
    {
        return false;
    }
}