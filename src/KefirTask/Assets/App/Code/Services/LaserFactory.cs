using App.Code.Components;
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

        public Entity Create(Vector3 position, Vector3 direction, Entity parent)
        {
            var e = Create(_laser, position, direction, parent);
            if (e.GetComponent<ColliderComponent>() is CapsuleColliderComponent capsule)
                capsule.MaxPoint = direction * 1000 - position;

            return e;
        }
    }
}