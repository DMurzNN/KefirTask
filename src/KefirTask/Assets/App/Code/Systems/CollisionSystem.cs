using System;
using App.Code.Components;
using App.Code.Core;
using App.Code.Systems.Physics;
using App.ECS;
using App.ECS.Components;

namespace App.Code.Systems
{
    public class CollisionSystem : ECS.System
    {
        public override Type[] Filters => new[]
            {typeof(PositionComponent), typeof(ColliderComponent), typeof(TagComponent)};

        public override void Execute(Entity[] entities)
        {
            for (var i = 0; i < entities.Length; i++)
            {
                for (var j = i; j < entities.Length; j++)
                {
                    var colliderA = entities[i].GetComponent<ColliderComponent>();
                    var tagA = entities[i].GetComponent<TagComponent>();
                    var positionA = entities[i].GetComponent<PositionComponent>();

                    var colliderB = entities[j].GetComponent<ColliderComponent>();
                    var tagB = entities[j].GetComponent<TagComponent>();
                    var positionB = entities[j].GetComponent<PositionComponent>();

                    if (tagA.Tag == tagB.Tag) continue;

                    if (Helper.Collide(positionA, colliderA, positionB, colliderB))
                    {
                        if (Helper.IsEnemyDamagedByBullet(tagA.Tag, tagB.Tag))
                        {
                            entities[j].AddComponent<DestroyComponent>();
                            entities[i].AddComponent<DestroyComponent>();
                        }

                        if (Helper.IsEnemyDamagedByLaser(tagA.Tag, tagB.Tag))
                        {
                            if (tagA.Tag == Tag.Enemy)
                                entities[i].AddComponent<DestroyComponent>();
                            else if (tagB.Tag == Tag.Enemy)
                                entities[j].AddComponent<DestroyComponent>();
                        }

                        if (Helper.IsPlayerDamaged(tagA.Tag, tagB.Tag))
                        {
                            if (tagA.Tag == Tag.Player)
                                entities[i].AddComponent<DestroyComponent>();
                            else if (tagB.Tag == Tag.Player)
                                entities[j].AddComponent<DestroyComponent>();
                        }

                        if (Helper.IsAsteroidDamagedByLaser(tagA.Tag, tagB.Tag))
                        {
                            if (tagA.Tag == Tag.Asteroid)
                                entities[i].AddComponent<DestroyComponent>();
                            else if (tagB.Tag == Tag.Asteroid)
                                entities[j].AddComponent<DestroyComponent>();
                        }

                        if (Helper.IsAsteroidDamagedByBullet(tagA.Tag, tagB.Tag))
                        {
                            if (tagA.Tag == Tag.Asteroid)
                            {
                                entities[i].AddComponent<CrashComponent>();
                                entities[j].AddComponent<DestroyComponent>();
                            }
                            else
                            {
                                entities[i].AddComponent<DestroyComponent>();
                                entities[j].AddComponent<CrashComponent>();
                            }
                        }
                    }
                }
            }
        }

        protected override void Execute(Entity entity)
        {
        }
    }
}