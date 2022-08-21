using App.Code.Components;
using App.Code.Core;
using App.ECS;
using App.ECS.Components;

namespace App.Code.Systems.Physics
{
    public static class CollideResults
    {
        public static void AsteroidPieceDamagedByLaser(Entity entityA, Tag tagA, Entity entityB, Tag tagB)
        {
            if (tagA == Tag.AsteroidPiece)
                entityA.AddComponent<DestroyByPlayerComponent>();
            else if (tagB == Tag.AsteroidPiece)
                entityB.AddComponent<DestroyByPlayerComponent>();
        }

        public static void AsteroidPieceDamagedByBullet(Tag tagA, Entity entityA, Entity entityB, Tag tagB)
        {
            if (tagA == Tag.AsteroidPiece)
            {
                entityA.AddComponent<DestroyByPlayerComponent>();
                entityB.AddComponent<DestroyComponent>();
            }
            else
            {
                entityB.AddComponent<DestroyByPlayerComponent>();
                entityA.AddComponent<DestroyComponent>();
            }
        }

        public static void AsteroidDamagedByBullet(Entity entityA, Tag tagA, Entity entityB, Tag tagB)
        {
            if (tagA == Tag.Asteroid)
            {
                entityA.AddComponent<CrashComponent>();
                entityA.AddComponent<DestroyByPlayerComponent>();
                entityB.AddComponent<DestroyComponent>();
            }
            else
            {
                entityA.AddComponent<DestroyComponent>();
                entityB.AddComponent<CrashComponent>();
                entityB.AddComponent<DestroyByPlayerComponent>();
            }
        }

        public static void AsteroidDamagedByLaser(Entity entityA, Tag tagA, Entity entityB, Tag tagB)
        {
            if (tagA == Tag.Asteroid)
                entityA.AddComponent<DestroyByPlayerComponent>();
            else if (tagB == Tag.Asteroid)
                entityB.AddComponent<DestroyByPlayerComponent>();
        }

        public static void PlayerDamaged(Entity entityA, Tag tagA, Entity entityB, Tag tagB)
        {
            if (tagA == Tag.Player)
            {
                entityA.AddComponent<PlayerDestroyComponent>();
                entityA.AddComponent<DestroyComponent>();
            }
            else if (tagB == Tag.Player)
            {
                entityB.AddComponent<PlayerDestroyComponent>();
                entityB.AddComponent<DestroyComponent>();
            }
        }

        public static void EnemyDamagedByLaser(Entity entityA, Tag tagA, Entity entityB, Tag tagB)
        {
            if (tagA == Tag.Enemy)
                entityA.AddComponent<DestroyByPlayerComponent>();
            else if (tagB == Tag.Enemy)
                entityB.AddComponent<DestroyByPlayerComponent>();
        }

        public static void EnemyDamageByBullet(Entity entityA, Tag tagA, Entity entityB, Tag tagB)
        {
            if (tagA == Tag.Enemy)
            {
                entityB.AddComponent<DestroyComponent>();
                entityA.AddComponent<DestroyByPlayerComponent>();
            }
            else
            {
                if (tagB == Tag.Enemy)
                {
                    entityA.AddComponent<DestroyComponent>();
                    entityB.AddComponent<DestroyByPlayerComponent>();
                }
            }
        }
    }
}