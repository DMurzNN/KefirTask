using System;
using App.ECS;

namespace App.Code.Components
{
    [Serializable]
    public class DynamicComponent : Component
    {
        public bool IsMoved;
        public bool IsDynamic;
    }
}