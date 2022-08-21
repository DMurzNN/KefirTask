using System;
using App.ECS;

namespace App.Code.Components
{
    [Serializable]
    public class DecelerationComponent : Component
    {
        public float Deceleration = 0.5f;
    }
}