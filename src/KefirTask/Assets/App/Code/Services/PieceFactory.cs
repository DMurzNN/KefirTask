using App.Code.Components;
using App.ECS;
using App.ECS.Prefab;
using UnityEngine;

namespace App.Code.Services
{
    public class PieceFactory : IPieceFactory
    {
        private readonly IEntityFactory _entityFactory;

        public PieceFactory(IEntityFactory entityFactory) => 
            _entityFactory = entityFactory;

        public Entity Create(PrefabEntity prefab, Vector3 position) =>
            _entityFactory
                .Create(prefab)
                .With(e => e.GetComponent<PositionComponent>().Position = position)
                .With(e => e.GetComponent<InfinityAccelerationComponent>().With(c => c.AccelerationDirection =
                    new Vector2().RandomDirection().To3D() * c.Acceleration));
    }
}