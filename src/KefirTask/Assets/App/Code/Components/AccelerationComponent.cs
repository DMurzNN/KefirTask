using System;
using UnityEngine;
using Component = App.ECS.Component;

namespace App.Code.Components
{
    [Serializable]
    public class AccelerationComponent : Component
    {
        public float Acceleration = 0.01f;
        public float Deceleration = 0.5f;
        public Vector3 AccelerationDirection;
    }
}