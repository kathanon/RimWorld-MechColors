using RimWorld;
using System.Runtime.CompilerServices;
using UnityEngine;
using Verse;

namespace MechColors;
public class State {
    private static readonly ConditionalWeakTable<Pawn, State> table = new();

    public Color? color;
    public readonly Pawn pawn;

    public static State For(Pawn pawn) 
        => (pawn?.IsColonyMech ?? false) ? table.GetValue(pawn, Create) : null;

    private static State Create(Pawn pawn) 
        => new(pawn);

    private State(Pawn pawn) 
        => this.pawn = pawn;

    public void ExposeData() 
        => Scribe_Values.Look(ref color, Strings.ID, null);

    public Gizmo Gizmo() 
        => new MechColorGizmo(this);

    public void Reset() 
        => Set(null);

    public void Set(Color? color) {
        this.color = color;
        PortraitsCache.SetDirty(pawn);
    }
}
