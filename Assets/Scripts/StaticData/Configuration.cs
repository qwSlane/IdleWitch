using UnityEngine;
using Witch.Components;

namespace Witch.StaticData
{
    [CreateAssetMenu]
    public class Configuration : ScriptableObject
    {
        public GlyphGeneration CurrentGeneration;
        
        public GlyphView GlyphPrefab;
    }
}
