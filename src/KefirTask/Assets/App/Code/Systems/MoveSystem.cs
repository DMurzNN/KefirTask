using System;
using App.Code.Components;
using App.Code.Services;
using App.ECS;
using UnityEngine;

namespace App.Code.Systems
{
    public class MoveSystem : ECS.System
    {
        private readonly WorldBoundsService _worldBoundsService;

        public MoveSystem(WorldBoundsService worldBoundsService) => 
            _worldBoundsService = worldBoundsService;

        public override Type[] Filters => new[] {typeof(SpeedComponent), typeof(PositionComponent)};
        
        protected override void Execute(Entity entity)
        {
            var speed = entity.GetComponent<SpeedComponent>().Speed;
            var positionComponent = entity.GetComponent<PositionComponent>();
            var axis = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            positionComponent.Position.x += axis.x * speed;
            positionComponent.Position.z += axis.y * speed;

            if (positionComponent.Position.x > _worldBoundsService.WorldBounds.x)
                positionComponent.Position.x = -_worldBoundsService.WorldBounds.x;
            
            if (positionComponent.Position.x < -_worldBoundsService.WorldBounds.x)
                positionComponent.Position.x = _worldBoundsService.WorldBounds.x;
            
            if (positionComponent.Position.z > _worldBoundsService.WorldBounds.y)
                positionComponent.Position.z = -_worldBoundsService.WorldBounds.y;
            
            if (positionComponent.Position.z < -_worldBoundsService.WorldBounds.y)
                positionComponent.Position.z = _worldBoundsService.WorldBounds.y;
        }
    }
}