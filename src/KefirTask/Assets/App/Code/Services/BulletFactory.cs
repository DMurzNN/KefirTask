using App.Code.Components;
using App.ECS;
using App.ECS.Prefab;
using UnityEngine;

namespace App.Code.Services
{
    public class BulletFactory : IBulletFactory
    {
        private readonly PrefabEntity _bullet;
        private readonly IEntityFactory _entityFactory;

        public BulletFactory(PrefabEntity bullet, IEntityFactory entityFactory)
        {
            _bullet = bullet;
            _entityFactory = entityFactory;
        }

        public Entity Create(Vector3 position, Vector3 direction, float acceleration)
        {
            var bulletEntity = _entityFactory.Create(_bullet);
            bulletEntity
                .GetComponent<PositionComponent>()
                .With(c => c.Position = position);
            bulletEntity
                .GetComponent<InfinityAccelerationComponent>()
                .With(c => c.Acceleration = acceleration)
                .With(c => c.AccelerationDirection = direction / 1000.0f);
            bulletEntity
                .GetComponent<ForwardComponent>()
                .With(c => c.Forward = direction);
            return bulletEntity;
        }
    }
}