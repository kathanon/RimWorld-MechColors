using HarmonyLib;
using System.Collections.Generic;
using Verse;

namespace MechColors;
[HarmonyPatch]
public static class Patch_Gizmo {
    [HarmonyPostfix]
    [HarmonyPatch(typeof(Pawn), nameof(Pawn.GetGizmos))]
    public static IEnumerable<Gizmo> GetGizmos(IEnumerable<Gizmo> result, Pawn __instance) {
        foreach (var gizmo in result) {
            yield return gizmo;
        }
        if (__instance.IsColonyMech) {
            yield return State.For(__instance).Gizmo();
        }
    }
}
