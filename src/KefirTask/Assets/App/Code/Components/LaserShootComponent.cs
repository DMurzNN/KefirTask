using System;
using App.ECS;

namespace App.Code.Components
{
    [Serializable]
    public class LaserShootComponent : Component
    {
        public int LaserCount;
        public float LaserRefillTime;
        public float RefillTimer;
    }
}