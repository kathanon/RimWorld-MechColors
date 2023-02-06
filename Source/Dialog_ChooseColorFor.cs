using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Verse;

namespace MechColors;
public class Dialog_ChooseColorFor : Dialog_ChooseColor {
    private static readonly List<Color> colors =
        DefDatabase<ColorDef>.AllDefsListForReading.Select(c => c.color).ToList();

    private readonly State state;
    private readonly Traverse<Color> selected;
    private bool useDefault;
    private Color curDefault = Find.FactionManager.OfPlayer.MechColor;

    private static Redirect redirect = new();

    public Dialog_ChooseColorFor(State state) : 
            base(Strings.ChooseColorFor(state.pawn), 
                 state.color ?? Find.FactionManager.OfPlayer.MechColor, 
                 colors,
                 redirect.Select) {
        this.state = state;
        useDefault = state.color == null;
        redirect.dialog = this;
        redirect = new();
        selected = Traverse.Create(this).Field<Color>("selectedColor");
    }

    public override void DoWindowContents(Rect inRect) {
        var prev = selected.Value;
        base.DoWindowContents(inRect);
        if (prev != selected.Value) useDefault = false;
        var rect = new Rect(inRect.x + 2f, inRect.y + 35f + 10f + 88f + 4f, 84f, 24f);
        Widgets.CheckboxLabeled(rect, Strings.Default, ref useDefault);
        if (useDefault) selected.Value = curDefault;
    }

    private void Select(Color col) {
        if (useDefault) {
            state.Reset();
        } else {
            state.Set(col);
        }
    }

    private class Redirect {
        public Dialog_ChooseColorFor dialog = null;

        public void Select(Color col) => dialog?.Select(col);
    }
}
