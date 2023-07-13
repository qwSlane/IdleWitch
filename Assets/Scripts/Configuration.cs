using UnityEngine;
using Witch.Systems;

namespace Witch
{
    [CreateAssetMenu]
    public class Configuration : ScriptableObject
    {
        public int Radius;
        public int PointsCount;

        public GlyphGeneration CurrentGeneration;
        
        public GlyphView GlyphPrefab;
    }
}
