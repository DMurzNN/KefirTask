using System;
using UnityEngine;
using Component = App.ECS.Component;

namespace App.Code.Components
{
    [Serializable]
    public class PositionComponent : Component
    {
        public Vector3 Position;
    }
}