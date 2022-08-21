using System;
using App.Code.Components;
using App.Code.Services;
using App.ECS;
using UnityEngine;

namespace App.Code.Systems
{
    public class AsteroidSpawnerSystem : ECS.System
    {
        private readonly ITimeService _timeService;
        private readonly IEntityFactory _entityFactory;
        private readonly IWorldBoundsService _worldBoundsService;

        public override Type[] Filters { get; } = {typeof(AsteroidSpawnerComponent)};

        public AsteroidSpawnerSystem(ITimeService timeService, IEntityFactory entityFactory,
            IWorldBoundsService worldBoundsService)
        {
            _timeService = timeService;
            _entityFactory = entityFactory;
            _worldBoundsService = worldBoundsService;
        }

        protected override void Execute(Entity entity)
        {
            var enemySpawner = entity.GetComponent<AsteroidSpawnerComponent>();

            if (enemySpawner.SpawnCooldown <= 0)
            {
                enemySpawner.SpawnCooldown = enemySpawner.SpawnTime;
                var enemy = _entityFactory
                    .Create(enemySpawner.EntityPrefab);
                enemy
                    .GetComponent<PositionComponent>()
                    .With(p => p.Position = _worldBoundsService.RandomPosition())
                    .With(p => p.PrevPosition = p.Position);
                enemy
                    .GetComponent<InfinityAccelerationComponent>()
                    .With(c => c.AccelerationDirection = new Vector2().RandomDirection().To3D() * c.Acceleration);
            }
            else
                enemySpawner.SpawnCooldown -= _timeService.DeltaTime;
        }
    }
}