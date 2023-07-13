using UnityEngine;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Witch.Services;

namespace Witch.Systems
{
    public class CreateGlyphViewSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<Glyph>, Exc<GlyphViewRef>> _filter;
        private readonly EcsPoolInject<Glyph> _glyphPool;
        private readonly EcsPoolInject<GlyphViewRef> _glyphRefPool;

        private readonly EcsCustomInject<Configuration> _configuration;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                _glyphRefPool.Value.Add(entity);

                ref Glyph glyph = ref _glyphPool.Value.Get(entity);

                GlyphView glyphView =
                    Object.Instantiate(_configuration.Value.GlyphPrefab, glyph.Position, Quaternion.identity);
                glyphView.Entity = entity;
            }
        }

      
    }
}