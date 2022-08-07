using System;
using App.ECS;

namespace App.Code.Components
{
    [Serializable]
    public class LifetimeComponent : Component
    {
        public float Lifetime;
        public float CurrentLifetime;
    }
}