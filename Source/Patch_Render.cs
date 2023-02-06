using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using Verse;

namespace MechColors;
[HarmonyPatch]
[StaticConstructorOnStartup]
public static class Patch_Render {
    private static Pawn pawn = null;
    private static readonly ConditionalWeakTable<Pawn, Dictionary<Material, Material>> dicts = new();
    private static Dictionary<Material, Material> origDict = null;

    [HarmonyPrefix]
    [HarmonyPatch(typeof(PawnRenderer), "DrawPawnBody")]
    public static void DrawPawnBody_Pre(Pawn ___pawn) {
        if (___pawn.IsColonyMech) pawn = ___pawn;
    }

    [HarmonyPostfix]
    [HarmonyPatch(typeof(PawnRenderer), "DrawPawnBody")]
    public static void DrawPawnBody_Post() {
        pawn = null;
    }

    [HarmonyPostfix]
    [HarmonyPatch(typeof(Faction), nameof(Faction.MechColor), MethodType.Getter)]
    public static void MechColor(ref Color __result) {
        var state = State.For(pawn);
        if (state != null) {
            var color = state.color ?? __result;
            __result = color;
        }
    }

    [HarmonyPrefix]
    [HarmonyPatch(typeof(PawnGraphicSet), nameof(PawnGraphicSet.GetOverlayMat))]
    public static void GetOverlayMat_Pre(ref Dictionary<Material, Material> ___overlayMats) {
        if (pawn != null) {
            origDict ??= ___overlayMats;
            ___overlayMats = dicts.GetOrCreateValue(pawn);
        }
    }

    [HarmonyPostfix]
    [HarmonyPatch(typeof(PawnGraphicSet), nameof(PawnGraphicSet.GetOverlayMat))]
    public static void GetOverlayMat_Post(ref Dictionary<Material, Material> ___overlayMats) {
        if (pawn != null) {
            ___overlayMats = origDict;
        }
    }
}
