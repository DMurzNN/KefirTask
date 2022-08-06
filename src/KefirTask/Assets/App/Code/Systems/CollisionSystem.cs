using System;
using App.Code.Components;
using App.Code.Core;
using App.ECS;
using App.ECS.Components;
using UnityEngine;

namespace App.Code.Systems
{
    public class CollisionSystem : ECS.System
    {
        public override Type[] Filters => new[]
            {typeof(PositionComponent), typeof(SphereColliderComponent), typeof(TagComponent)};

        public override void Execute(Entity[] entities)
        {
            for (var i = 0; i < entities.Length; i++)
            {
                for (var j = i; j < entities.Length; j++)
                {
                    var colliderA = entities[i].GetComponent<SphereColliderComponent>();
                    var tagA = entities[i].GetComponent<TagComponent>();
                    var positionA = entities[i].GetComponent<PositionComponent>();

                    var colliderB = entities[j].GetComponent<SphereColliderComponent>();
                    var tagB = entities[j].GetComponent<TagComponent>();
                    var positionB = entities[j].GetComponent<PositionComponent>();

                    if (tagA.Tag == tagB.Tag) continue;

                    if (Collide(positionA, colliderA, positionB, colliderB))
                    {
                        if (IsEnemyDamaged(tagA.Tag, tagB.Tag))
                        {
                            entities[j].AddComponent<DestroyComponent>();
                            entities[i].AddComponent<DestroyComponent>();
                        }

                        if (IsPlayerDamaged(tagA.Tag, tagB.Tag))
                        {
                            if (tagA.Tag == Tag.Player)
                                entities[i].AddComponent<DestroyComponent>();
                            else if (tagB.Tag == Tag.Player)
                                entities[j].AddComponent<DestroyComponent>();
                        }
                    }
                }
            }
        }

        protected override void Execute(Entity entity)
        {
        }

        private bool Collide(PositionComponent positionA, SphereColliderComponent colliderA,
            PositionComponent positionB, SphereColliderComponent colliderB)
        {
            var dist = Vector3.Distance(colliderA.Center + positionA.Position, colliderB.Center + positionB.Position);
            return dist <= colliderA.Radius + colliderB.Radius;
        }

        private static bool IsEnemyDamaged(Tag tagA, Tag tagB) =>
            (tagA == Tag.Bullet && tagB == Tag.Enemy) ||
            (tagB == Tag.Bullet && tagA == Tag.Enemy);

        private static bool IsPlayerDamaged(Tag tagA, Tag tagB) =>
            (tagA == Tag.Player && tagB == Tag.Enemy) ||
            (tagB == Tag.Player && tagA == Tag.Enemy);
    }
}