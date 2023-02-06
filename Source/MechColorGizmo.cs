using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace MechColors;
public class MechColorGizmo : Command_Action {
    private readonly State state;

    public MechColorGizmo(State state) {
        this.state = state;
        defaultLabel = Strings.SetColorLabel;
        defaultDesc = Strings.SetColorDesc;
        icon = Textures.ChangeColor;
        action = OnClicked;
    }

    public override IEnumerable<FloatMenuOption> RightClickFloatMenuOptions {
        get {
            yield return new FloatMenuOption(Strings.SelectColor,  OnClicked);
            yield return new FloatMenuOption(Strings.DefaultColor, state.Reset);
        }
    }

    public override void DrawIcon(Rect rect, Material buttonMat, GizmoRenderParms parms) {
        base.DrawIcon(rect, buttonMat, parms);
        var color = state.color;
        if (color != null) {
            rect.xMin += rect.width  * .6f;
            rect.yMax -= rect.height * .6f;
            rect = rect.ContractedBy(6f);
            GUI.color = color.Value;
            GUI.DrawTexture(rect, Textures.Circle);
            GUI.color = Color.white;
        }
    }

    private void OnClicked() {
        Find.WindowStack.Add(new Dialog_ChooseColorFor(state));
    }
}
