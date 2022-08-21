using System;
using App.Code.Components;
using App.ECS;
using UnityEngine;

namespace App.Code.Systems
{
    public class UpdateCapsuleColliderSystem : ECS.System
    {
        public override Type[] Filters { get; } =
            {typeof(ColliderComponent), typeof(ForwardComponent)};

        protected override void Execute(Entity entity)
        {
            var collider = entity.GetComponent<ColliderComponent>();
            var forward = entity.GetComponent<ForwardComponent>();

            if (collider is not CapsuleColliderComponent capsuleCollider) return;

            var colliderSize = Vector3.Distance(capsuleCollider.MaxPoint, capsuleCollider.MinPoint);
            capsuleCollider.MaxPoint = forward.Forward * colliderSize;
        }
    }
}