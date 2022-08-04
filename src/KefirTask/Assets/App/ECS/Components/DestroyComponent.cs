using System;

namespace App.ECS.Components
{
    [Serializable]
    public class DestroyComponent : Component
    {
        public bool Destroyed;
    }
}