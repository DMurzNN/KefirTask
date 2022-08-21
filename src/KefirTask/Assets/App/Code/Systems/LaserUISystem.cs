using System;
using App.Code.Components;
using App.Code.Core.UI;
using App.ECS;

namespace App.Code.Systems
{
    public class LaserUISystem : ECS.System
    {
        public override Type[] Filters { get; } = {typeof(LaserShootComponent)};
        
        private readonly Mediator _mediator;

        public LaserUISystem(Mediator mediator) =>
            _mediator = mediator;

        protected override void Execute(Entity entity)
        {
            var laser = entity.GetComponent<LaserShootComponent>();

            _mediator.UpdateLaserCooldown(laser.RefillTimer, laser.LaserRefillTime);
            _mediator.UpdateLaserCount(laser.LaserCount);
        }
    }
}