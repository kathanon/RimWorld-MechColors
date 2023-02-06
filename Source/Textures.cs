using UnityEngine;
using Verse;

namespace MechColors {
    [StaticConstructorOnStartup]
    public static class Textures {
        private const string Prefix = Strings.ID + "/";

        // Local
        public static readonly Texture2D Circle = ContentFinder<Texture2D>.Get(Prefix + "Circle");

        // Vanilla
        public static readonly Texture2D ChangeColor = ContentFinder<Texture2D>.Get("UI/Commands/ChangeColor");
    }
}
