using System;
using App.Code.Components;
using App.Code.Systems.Physics;
using App.ECS;

namespace App.Code.Systems
{
    public class CollisionSystem : ECS.System
    {
        public override Type[] Filters { get; } = 
            {typeof(PositionComponent), typeof(ColliderComponent), typeof(TagComponent)};

        public override void Execute(Entity[] entities)
        {
            for (var i = 0; i < entities.Length; i++)
            {
                for (var j = i; j < entities.Length; j++)
                {
                    var entityA = entities[i];
                    var colliderA = entityA.GetComponent<ColliderComponent>();
                    var tagA = entityA.GetComponent<TagComponent>();
                    var positionA = entityA.GetComponent<PositionComponent>();

                    var entityB = entities[j];
                    var colliderB = entityB.GetComponent<ColliderComponent>();
                    var tagB = entityB.GetComponent<TagComponent>();
                    var positionB = entityB.GetComponent<PositionComponent>();

                    if (tagA.Tag == tagB.Tag) continue;

                    if (PhysicsHelper.Collide(positionA, colliderA, positionB, colliderB))
                        CalculateCollideResult(entityA, tagA, entityB, tagB);
                }
            }
        }

        protected override void Execute(Entity entity)
        {
        }

        private static void CalculateCollideResult(Entity entityA, TagComponent tagA, Entity entityB, TagComponent tagB)
        {
            if (PhysicsHelper.IsEnemyDamagedByBullet(tagA.Tag, tagB.Tag))
                CollideResults.EnemyDamageByBullet(entityA, tagA.Tag, entityB, tagB.Tag);
            else if (PhysicsHelper.IsEnemyDamagedByLaser(tagA.Tag, tagB.Tag))
                CollideResults.EnemyDamagedByLaser(entityA, tagA.Tag, entityB, tagB.Tag);
            else if (PhysicsHelper.IsPlayerDamaged(tagA.Tag, tagB.Tag))
                CollideResults.PlayerDamaged(entityA, tagA.Tag, entityB, tagB.Tag);
            else if (PhysicsHelper.IsAsteroidDamagedByLaser(tagA.Tag, tagB.Tag))
                CollideResults.AsteroidDamagedByLaser(entityA, tagA.Tag, entityB, tagB.Tag);
            else if (PhysicsHelper.IsAsteroidDamagedByBullet(tagA.Tag, tagB.Tag))
                CollideResults.AsteroidDamagedByBullet(entityA, tagA.Tag, entityB, tagB.Tag);
            else if (PhysicsHelper.IsAsteroidPieceDamagedByBullet(tagA.Tag, tagB.Tag))
                CollideResults.AsteroidPieceDamagedByBullet(tagA.Tag, entityA, entityB, tagB.Tag);
            else if (PhysicsHelper.IsAsteroidPieceDamagedByLaser(tagA.Tag, tagB.Tag)) 
                CollideResults.AsteroidPieceDamagedByLaser(entityA, tagA.Tag, entityB, tagB.Tag);
        }
    }
}