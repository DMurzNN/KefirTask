using System;
using App.ECS;

namespace App.Code.Components
{
    [Serializable]
    public class RotateSpeedComponent : Component
    {
        public float Speed;
    }
}