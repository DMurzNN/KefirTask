using System;
using App.ECS;

namespace App.Code.Components
{
    [Serializable]
    public class BulletShootComponent : Component
    {
        public float BulletAcceleration = 0.5f;
        public int BulletCount;
        public float ShootTime;
    }
}