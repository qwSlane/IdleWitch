using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;
using Witch.Services;

namespace Witch.Systems
{
    public class InputSystem : IEcsRunSystem
    {
        private readonly EcsWorldInject _world;
        private readonly EcsPoolInject<Glyph> _glyphPool;
        private readonly EcsPoolInject<GlyphsOverflow> _overflowPool;
        private readonly EcsPoolInject<GlyphViewRef> _glyphRefPool;

        private readonly EcsCustomInject<Configuration> _configuration;
        private readonly EcsCustomInject<SceneData> _sceneData;

        public void Run(IEcsSystems systems)
        {
            if (Input.GetMouseButtonDown(0))
            {
                var entity = _world.Value.NewEntity();
                
                if (_sceneData.Value.Glyphs == _configuration.Value.PointsCount)
                {
                    _overflowPool.Value.Add(entity);
                }
                else
                {
                    _glyphPool.Value.Add(entity);
                }

            }
        }
    }

    public struct GlyphsOverflow
    {
    }
}