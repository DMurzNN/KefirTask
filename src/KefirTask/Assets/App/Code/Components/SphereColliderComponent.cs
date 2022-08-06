using System;
using UnityEngine;
using Component = App.ECS.Component;

namespace App.Code.Components
{
    [Serializable]
    public class SphereColliderComponent : Component
    {
        public Vector3 Center;
        public float Radius;
    }
}