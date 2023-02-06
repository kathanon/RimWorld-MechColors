using RimWorld;
using Verse;

namespace MechColors {
    public static class Strings {
        public const string ID = "kathanon.MechColors";
        public const string Name = "Mech Colors";

        public static readonly string Default       = (ID + ".Default"      ).Translate();
        public static readonly string SetColorLabel = (ID + ".SetColorLabel").Translate();
        public static readonly string SetColorDesc  = (ID + ".SetColorDesc" ).Translate();
        public static readonly string SelectColor   = (ID + ".SelectColor"  ).Translate();
        public static readonly string DefaultColor  = (ID + ".DefaultColor" ).Translate();


        public const string ChooseColorKey = ID + ".ChooseColor";

        public static string ChooseColorFor(Pawn pawn) 
            => ChooseColorKey.Translate(pawn.LabelShortCap);
    }
}
