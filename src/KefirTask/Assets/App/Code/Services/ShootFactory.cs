using App.Code.Components;
using App.ECS;
using App.ECS.Prefab;
using UnityEngine;

namespace App.Code.Services
{
    public abstract class ShootFactory
    {
        private readonly IEntityFactory _entityFactory;

        protected ShootFactory(IEntityFactory entityFactory) =>
            _entityFactory = entityFactory;

        protected Entity Create(PrefabEntity prefab, Vector3 position, Vector3 direction, Entity parent = null)
        {
            var entity = _entityFactory.Create(prefab, parent);
            entity
                .GetComponent<PositionComponent>()
                .With(c => c.Position = position);
            entity
                .GetComponent<ForwardComponent>()
                .With(c => c.Forward = direction.normalized);
            entity.With(e => e
                    .GetComponent<InfinityAccelerationComponent>()
                    .With(c => c.AccelerationDirection = direction * c.Acceleration),
                entity.HasComponent<InfinityAccelerationComponent>());
            return entity;
        }
    }
}