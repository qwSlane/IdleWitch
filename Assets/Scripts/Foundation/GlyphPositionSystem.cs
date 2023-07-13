using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;
using Witch.Services;
using Witch.Systems;

namespace Witch.Foundation
{
    public class GlyphPositionSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<Glyph>, Exc<GlyphViewRef>> _filter;
        private readonly EcsPoolInject<Glyph> _glyphPool;
        
        private readonly EcsCustomInject<Configuration> _configuration;
        private readonly EcsCustomInject<SceneData> _sceneData;
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                ref Glyph glyph = ref _glyphPool.Value.Get(entity);
                
                var angle = glyph.Number * 360 / _configuration.Value.PointsCount;

                var center = _sceneData.Value.glyphCenter.position;
                var x = center.x + _configuration.Value.Radius * Mathf.Cos(angle);
                var z = center.z + _configuration.Value.Radius * Mathf.Sin(angle);

                glyph.Position = new Vector3(x, 1, z);

            }

        }
    }
}