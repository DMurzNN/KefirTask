using System;
using App.Code.Components;
using App.ECS;
using UnityEngine;

namespace App.Code.Systems
{
    public class CollisionSystem : ECS.System
    {
        public override Type[] Filters => new[] {typeof(CollisionComponent)};

        protected override void Execute(Entity entity)
        {
            var collisionComponent = entity.GetComponent<CollisionComponent>();
            if (collisionComponent.CollisionAdapter.IsCollide)
                Debug.Log("Collide");
        }
    }
}