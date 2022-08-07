using System;
using UnityEngine;
using Component = App.ECS.Component;

namespace App.Code.Components
{
    [Serializable]
    public class ColliderComponent : Component
    {
        public Vector3 Center;
    }
}