using System;
using UnityEngine;
using Component = App.ECS.Component;

namespace App.Code.Components
{
    [Serializable]
    public class InfinityAccelerationComponent : Component
    {
        public float Acceleration = 0.01f;
        public Vector3 AccelerationDirection;
    }
}