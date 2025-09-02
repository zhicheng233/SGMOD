using HarmonyLib;

namespace SGMOD.Core.Fix;

public class FocusTarget {
    [HarmonyPrefix]
    [HarmonyPatch(typeof(MAI2System.Config), "get_IsTarget")]
    private static bool get_IsTarget(ref bool __result)
    {
        __result = false;
        return false;
    }
}