using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;
using Witch.Components;
using Witch.StaticData;

namespace Witch.Systems
{
    public class InputSystem : IEcsRunSystem
    {
        private readonly EcsWorldInject _world;
        private readonly EcsPoolInject<Glyph> _glyphPool;
        private readonly EcsPoolInject<Disappearing> _disappearPool;

        private readonly EcsFilterInject<Inc<GlyphViewRef>, Exc<Vanished, Disappearing>> _glyphRefPool;
        
        private readonly EcsCustomInject<SceneData> _sceneData;

        public void Run(IEcsSystems systems)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (++_sceneData.Value.Сircle.GlyphsCount == _sceneData.Value.Сircle.CircleSize)
                {
                    foreach (var glyph in _glyphRefPool.Value)
                    {
                        _disappearPool.Value.Add(glyph);
                    }
                    _sceneData.Value.Сircle.GlyphsCount = -1;
                }
                else
                {
                    var entity = _world.Value.NewEntity();
                    _glyphPool.Value.Add(entity);
                    ref Glyph glyph = ref _glyphPool.Value.Get(entity);

                    glyph.Number = _sceneData.Value.Сircle.GlyphsCount;
                }

            }
        }
    }
}