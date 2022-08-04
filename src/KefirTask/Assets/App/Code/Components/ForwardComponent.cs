using System;
using UnityEngine;
using Component = App.ECS.Component;

namespace App.Code.Components
{
    [Serializable]
    public class ForwardComponent : Component
    {
        public Vector3 Forward = Vector3.forward;
    }
}