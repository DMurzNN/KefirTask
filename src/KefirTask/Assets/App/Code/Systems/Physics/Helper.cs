using System.Collections.Generic;
using App.Code.Components;
using App.Code.Core;
using App.ECS;
using UnityEngine;

namespace App.Code.Systems.Physics
{
    public static class Helper
    {
        public const bool InterpolateCollision = true;
        public const float InterpolateStep = 0.1f;

        public static bool Collide(PositionComponent positionA, ColliderComponent colliderA,
            PositionComponent positionB, ColliderComponent colliderB)
        {
            if (colliderA is SphereColliderComponent sphereA && colliderB is SphereColliderComponent sphereB)
                return IsCollideSpheres(positionA, sphereA, positionB, sphereB);

            if (colliderA is CapsuleColliderComponent capsuleA && colliderB is SphereColliderComponent sphereBwc)
                return IsCapsuleAndSphereCollide(positionA, capsuleA, positionB, sphereBwc);

            if (colliderA is SphereColliderComponent sphereAwc && colliderB is CapsuleColliderComponent capsuleBwc)
                return IsCapsuleAndSphereCollide(positionB, capsuleBwc, positionA, sphereAwc);

            return false;
        }

        public static List<Entity> SphereCastAll(Vector3 from, Vector3 direction, float radius, float distance,
            Entity[] entities)
        {
            var collideEntities = new List<Entity>(entities.Length);
            var maxPoint = direction.normalized * distance;

            var lerpValue = InterpolateStep;
            while (lerpValue < 1.0f)
            {
                var positionLerp = from.Lerp(maxPoint, lerpValue);

                foreach (var e in entities)
                {
                    var collider = e.GetComponent<SphereColliderComponent>();
                    var position = e.GetComponent<PositionComponent>();

                    if (CheckCollision(positionLerp, collider.Center + position.Position,
                            radius + collider.Radius))
                        collideEntities.Add(e);
                }

                lerpValue += InterpolateStep;
            }

            return collideEntities;
        }

        public static Entity SphereCast(Vector3 from, Vector3 direction, float radius, float distance,
            Entity[] entities)
        {
            var maxPoint = direction.normalized * distance;

            var lerpValue = InterpolateStep;
            while (lerpValue < 1.0f)
            {
                var positionLerp = from.Lerp(maxPoint, lerpValue);

                foreach (var e in entities)
                {
                    var collider = e.GetComponent<SphereColliderComponent>();
                    var position = e.GetComponent<PositionComponent>();

                    if (CheckCollision(positionLerp, collider.Center + position.Position,
                            radius + collider.Radius))
                        return e;
                }

                lerpValue += InterpolateStep;
            }

            return null;
        }

        public static bool CheckCollision(Vector3 colliderA, Vector3 colliderB, float radius) =>
            Vector3.Distance(colliderA, colliderB) <= radius;

        public static bool IsAsteroidDamagedByLaser(Tag tagA, Tag tagB) =>
            (tagB == Tag.Laser && tagA == Tag.Asteroid) ||
            (tagA == Tag.Laser && tagB == Tag.Asteroid);

        public static bool IsAsteroidDamagedByBullet(Tag tagA, Tag tagB) =>
            (tagB == Tag.Bullet && tagA == Tag.Asteroid) ||
            (tagA == Tag.Bullet && tagB == Tag.Asteroid);

        public static bool IsPlayerDamaged(Tag tagA, Tag tagB) =>
            (tagA == Tag.Player && tagB == Tag.Enemy) ||
            (tagB == Tag.Player && tagA == Tag.Enemy) ||
            (tagA == Tag.Player && tagB == Tag.Asteroid) ||
            (tagB == Tag.Player && tagA == Tag.Asteroid);

        public static bool IsEnemyDamagedByBullet(Tag tagA, Tag tagB) =>
            (tagA == Tag.Bullet && tagB == Tag.Enemy) ||
            (tagB == Tag.Bullet && tagA == Tag.Enemy);

        public static bool IsEnemyDamagedByLaser(Tag tagA, Tag tagB) =>
            (tagB == Tag.Laser && tagA == Tag.Enemy) ||
            (tagA == Tag.Laser && tagB == Tag.Enemy);

        private static bool IsCollideSpheres(PositionComponent positionA, SphereColliderComponent colliderA,
            PositionComponent positionB, SphereColliderComponent colliderB)
        {
            var colliderRadius = colliderA.Radius + colliderB.Radius;
            var isCollide = CheckCollision(colliderA.Center + positionA.Position, colliderB.Center + positionB.Position,
                colliderRadius);

            if (!isCollide && InterpolateCollision)
            {
                var lerpValue = InterpolateStep;
                while (lerpValue < 1.0f && !isCollide)
                {
                    var positionALerp = positionA.PrevPosition.Lerp(positionA.Position, lerpValue);
                    var positionBLerp = positionB.PrevPosition.Lerp(positionB.Position, lerpValue);

                    isCollide = CheckCollision(colliderA.Center + positionALerp, colliderB.Center + positionBLerp,
                        colliderRadius);

                    lerpValue += InterpolateStep;
                }
            }

            return isCollide;
        }

        private static bool IsCapsuleAndSphereCollide(PositionComponent positionA, CapsuleColliderComponent capsuleA,
            PositionComponent positionB, SphereColliderComponent sphereB)
        {
            var colliderRadius = capsuleA.Radius + sphereB.Radius;
            return positionB.Position.DistanceToLine(capsuleA.MinPoint + positionA.Position,
                capsuleA.MaxPoint + positionA.Position) <= colliderRadius;
        }
    }
}