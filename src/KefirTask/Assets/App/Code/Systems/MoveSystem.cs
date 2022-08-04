using System;
using App.Code.Components;
using App.ECS;
using UnityEngine;

namespace App.Code.Systems
{
    public class MoveSystem : ECS.System
    {
        public override Type[] Filters => new[] {typeof(SpeedComponent), typeof(PositionComponent)};
        protected override void Execute(Entity entity)
        {
            var speed = entity.GetComponent<SpeedComponent>().Speed;
            var positionComponent = entity.GetComponent<PositionComponent>();
            var axis = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            positionComponent.Position.x += axis.x * speed;
            positionComponent.Position.z += axis.y * speed;
        }
    }
}