using System;
using Component = App.ECS.Component;

namespace App.Code.Components
{
    [Serializable]
    public class FollowComponent : Component
    {
        public float Speed;
        public PositionComponent TargetPosition;
    }
}