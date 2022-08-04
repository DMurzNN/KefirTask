using System;
using App.ECS;

namespace App.Code.Components
{
    [Serializable]
    public class RotateComponent : Component
    {
        public float YAngle;
    }
}