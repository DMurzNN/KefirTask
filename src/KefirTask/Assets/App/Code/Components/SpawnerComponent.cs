using System;
using App.ECS;
using App.ECS.Prefab;

namespace App.Code.Components
{
    [Serializable]
    public abstract class SpawnerComponent : Component
    {
        public float SpawnTime;
        public float SpawnCooldown;
        public PrefabEntity EntityPrefab;
    }
}