using System;
using UnityEngine;

namespace App.Code.Components
{
    [Serializable]
    public class CapsuleColliderComponent : ColliderComponent
    {
        public float Radius;
        public Vector3 MaxPoint;
        public Vector3 MinPoint;
    }
}