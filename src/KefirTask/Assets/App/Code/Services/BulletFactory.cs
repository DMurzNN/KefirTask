using App.ECS;
using App.ECS.Prefab;
using UnityEngine;

namespace App.Code.Services
{
    public class BulletFactory : ShootFactory, 
        IBulletFactory
    {
        private readonly PrefabEntity _bullet;

        public BulletFactory(PrefabEntity bullet, IEntityFactory entityFactory) : base(entityFactory) => 
            _bullet = bullet;

        public Entity Create(Vector3 position, Vector3 direction) => 
            Create(_bullet, position, direction);
    }
}