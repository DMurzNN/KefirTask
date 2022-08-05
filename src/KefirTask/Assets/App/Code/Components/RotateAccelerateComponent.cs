using System;
using App.ECS;

namespace App.Code.Components
{
    [Serializable]
    public class RotateAccelerateComponent : Component
    {
        public float AccelerationDirection;
        public float Accelerate = 0.5f;
        public float Decelerate = 0.5f;
    }
}