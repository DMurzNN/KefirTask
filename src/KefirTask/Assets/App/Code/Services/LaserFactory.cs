using App.ECS;
using App.ECS.Prefab;
using UnityEngine;

namespace App.Code.Services
{
    public class LaserFactory : ShootFactory, 
        ILaserFactory
    {
        private readonly PrefabEntity _laser;

        public LaserFactory(PrefabEntity laser, IEntityFactory entityFactory) : base(entityFactory) => 
            _laser = laser;

        public Entity Create(Vector3 position, Vector3 direction) => 
            Create(_laser, position, direction);
    }
}