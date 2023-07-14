using UnityEngine;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Witch.Components;
using Witch.StaticData;

namespace Witch.Systems
{
    public class CreateGlyphViewSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<Glyph>, Exc<GlyphViewRef>> _filter;
        private readonly EcsFilterInject<Inc<Vanished>> _vanishedFilter;

        private readonly EcsPoolInject<Glyph> _glyphPool;
        private readonly EcsPoolInject<GlyphViewRef> _glyphRefPool;
        private readonly EcsPoolInject<Vanished> _vanishedPool;

        private readonly EcsCustomInject<Configuration> _configuration;
        private readonly EcsCustomInject<SceneData> _sceneData;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                ref Glyph glyph = ref _glyphPool.Value.Get(entity);
                var position = _sceneData.Value.Сircle.Centre.position.PositionOnSphere(glyph.Number,
                    _sceneData.Value.Сircle.CircleSize, _sceneData.Value.Сircle.Radius);
                
                if (_vanishedFilter.Value.GetEntitiesCount() != 0)
                {
                    GetPooledView(position);
                }
                else
                {
                    CreateView(entity, position);
                }
            }
        }

        private void CreateView(int entity, Vector3 position)
        {
            _glyphRefPool.Value.Add(entity);

            GlyphView glyphView =
                Object.Instantiate(_configuration.Value.GlyphPrefab, position, Quaternion.identity);
            glyphView.Entity = entity;

            ref GlyphViewRef glyphRef = ref _glyphRefPool.Value.Get(entity);
            glyphRef.Value = glyphView;
        }

        private void GetPooledView(Vector3 position)
        {
            foreach (var entity in _vanishedFilter.Value)
            {
                ref GlyphViewRef glyphRef = ref _glyphRefPool.Value.Get(entity);
                    
                _vanishedPool.Value.Del(entity);
                glyphRef.Value.gameObject.SetActive(true);
                
                var transform = glyphRef.Value.transform;
                transform.position = position;
                transform.localScale = Vector3.one;
                return;
            }
        }

      
    }
}