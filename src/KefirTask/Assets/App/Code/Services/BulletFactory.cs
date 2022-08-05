using App.Code.Adapters;
using App.Code.Components;
using App.ECS;
using UnityEngine;

namespace App.Code.Services
{
    public class BulletFactory : IBulletFactory
    {
        private readonly Bullet _bullet;
        private readonly IEntityFactory _entityFactory;

        public BulletFactory(Bullet bullet, IEntityFactory entityFactory)
        {
            _bullet = bullet;
            _entityFactory = entityFactory;
        }

        public Entity Create(Vector3 position, Vector3 direction, float acceleration)
        {
            var bullet = Object.Instantiate(_bullet);
            return _entityFactory.Create($"Bullet {System.Guid.NewGuid().ToString()}")
                .With(new PositionComponent
                {
                    Position = position
                })
                .With(new InfinityAccelerationComponent
                {
                    Acceleration = acceleration,
                    AccelerationDirection = direction / 1000.0f
                })
                .With(new CollisionComponent
                {
                    CollisionAdapter = bullet.CollisionAdapter
                })
                .With(new ForwardComponent
                {
                    Forward = direction
                })
                .LinkWith(bullet.gameObject);
        }
    }
}