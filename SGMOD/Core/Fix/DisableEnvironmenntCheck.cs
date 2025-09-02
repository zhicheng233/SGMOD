using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;
using MelonLoader;
using Process;

namespace SGMOD.Core.Fix;

public class DisableEnvironmenntCheck {
    [HarmonyTranspiler]
    [HarmonyPatch(typeof(WarningProcess), "OnStart")]
    public static IEnumerable<CodeInstruction> OnStart(IEnumerable<CodeInstruction> instructions,ILGenerator generator) {
        var codes = new List<CodeInstruction>(instructions);
        // 匹配 IL_00B9
        int targetIndex = codes.FindIndex(ci =>
            ci.opcode == OpCodes.Ldsfld &&
            ci.operand is FieldInfo fi &&
            fi.Name == "OnceDisp" &&
            fi.DeclaringType.FullName.Contains("Process.WarningProcess"));

        if (targetIndex == -1) {
            MelonLogger.Warning("[DisableEnvironmenntCheck]Failed to find the target instruction.");
            return codes;
        }
        
        var label = generator.DefineLabel();
        codes[targetIndex].labels.Add(label);
        
        var brInst = new CodeInstruction(OpCodes.Br, label);
        codes.Insert(0, brInst);

        return codes;
    }
}