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

        protected Entity Create(PrefabEntity prefab, Vector3 position, Vector3 direction)
        {
            var entity = _entityFactory.Create(prefab);
            entity
                .GetComponent<PositionComponent>()
                .With(c => c.Position = position);
            entity
                .GetComponent<InfinityAccelerationComponent>()
                .With(c => c.AccelerationDirection = direction / 1000.0f);
            entity
                .GetComponent<ForwardComponent>()
                .With(c => c.Forward = direction);
            return entity;
        }
    }
}