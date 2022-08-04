using System;
using App.Code.Adapters;
using App.ECS;

namespace App.Code.Components
{
    [Serializable]
    public class CollisionComponent : Component
    {
        public CollisionAdapter CollisionAdapter;
    }
}