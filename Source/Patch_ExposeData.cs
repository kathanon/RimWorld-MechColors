using HarmonyLib;
using Verse;

namespace MechColors;
[HarmonyPatch]
public static class Patch_ExposeData {
    [HarmonyPostfix]
    [HarmonyPatch(typeof(Pawn), nameof(Pawn.ExposeData))]
    public static void ExposeData(Pawn __instance) {
        State.For(__instance)?.ExposeData();
    }
}
