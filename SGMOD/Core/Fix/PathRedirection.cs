
using HarmonyLib;
using Path = MAI2System.Path;

namespace SGMOD.Core.Fix;

public class PathRedirection {
    [HarmonyPrefix]
    [HarmonyPatch(typeof(Path), "get_BinPath")]
    public static bool get_BinPath(ref string __result) {
        __result = "./bin";
        return false;
    }  
    [HarmonyPrefix]
    [HarmonyPatch(typeof(Path), "get_FirmPath")]
    public static bool get_FirmPath(ref string __result) {
        __result = "./firm";
        return false;
    }  
    [HarmonyPrefix]
    [HarmonyPatch(typeof(Path), "get_ErrorLogPath")]
    public static bool get_ErrorLogPath(ref string __result) {
        __result = "./Errorlog";
        return false;
    }  
    [HarmonyPrefix]
    [HarmonyPatch(typeof(Path), "get_PhotoPath")]
    public static bool get_PhotoPath(ref string __result) {
        __result = "./FaceIconCapture";
        return false;
    }  
    [HarmonyPrefix]
    [HarmonyPatch(typeof(Path), "get_UploadPath")]
    public static bool get_UploadPath(ref string __result) {
        __result = "./UploadData";
        return false;
    }  
}