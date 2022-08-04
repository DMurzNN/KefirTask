using System;
using App.Code.Components;
using App.ECS;
using UnityEngine;

namespace App.Code.Systems
{
    public class ForwardSystem : ECS.System
    {
        public override Type[] Filters => new[] {typeof(ForwardComponent), typeof(RotateComponent)};

        protected override void Execute(Entity entity)
        {
            var forward = entity.GetComponent<ForwardComponent>();
            var rotate = entity.GetComponent<RotateComponent>();

            var radAngle = (-rotate.YAngle).ToRadians() + Mathf.PI / 2;
            forward.Forward = new Vector2(Mathf.Cos(radAngle), Mathf.Sin(radAngle)).To3D();
        }
    }
}