using HarmonyLib;
using Verse;

namespace MechColors {
    [StaticConstructorOnStartup]
    public class Main : Mod {
        public static Main Instance { get; private set; }

        static Main() {
            var harmony = new Harmony(Strings.ID);
            harmony.PatchAll();
        }

        public Main(ModContentPack content) : base(content) {
            Instance = this;
        }
    }
}
