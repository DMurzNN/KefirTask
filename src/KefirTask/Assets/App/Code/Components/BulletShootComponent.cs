using System;
using App.ECS;

namespace App.Code.Components
{
    [Serializable]
    public class BulletShootComponent : Component
    {
        public int BulletCount;
        public float ShootTime;
    }
}