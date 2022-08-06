using System;
using App.Code.Components;
using App.Code.Services;
using App.ECS;

namespace App.Code.Systems
{
    public class EnemySpawnerSystem : ECS.System
    {
        private readonly ITimeService _timeService;
        private readonly IEntityFactory _entityFactory;
        private readonly IWorldBoundsService _worldBoundsService;

        public override Type[] Filters => new[] {typeof(EnemySpawnerComponent)};

        public EnemySpawnerSystem(ITimeService timeService, IEntityFactory entityFactory, IWorldBoundsService worldBoundsService)
        {
            _timeService = timeService;
            _entityFactory = entityFactory;
            _worldBoundsService = worldBoundsService;
        }

        protected override void Execute(Entity entity)
        {
            var enemySpawner = entity.GetComponent<EnemySpawnerComponent>();

            if (enemySpawner.SpawnCooldown <= 0)
            {
                enemySpawner.SpawnCooldown = enemySpawner.SpawnTime;
                var enemy = _entityFactory
                    .Create(enemySpawner.EntityPrefab);
                enemy
                    .GetComponent<FollowComponent>()
                    .With(c => c.TargetPosition = enemySpawner.PlayerPosition);
                enemy
                    .GetComponent<PositionComponent>()
                    .With(p => p.Position = _worldBoundsService.WorldBounds.To3D().Random());
            }
            else
                enemySpawner.SpawnCooldown -= _timeService.DeltaTime;
        }
    }
}