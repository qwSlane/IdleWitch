using UnityEngine;

namespace Witch.Systems
{
    public struct Glyph
    {
        public int Number;
        public Vector3 Position;
    }

    public enum GlyphGeneration
    {
        None,
        Common,
    }
}