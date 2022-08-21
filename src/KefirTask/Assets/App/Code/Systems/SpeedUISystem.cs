using System;
using App.Code.Components;
using App.Code.Core.UI;
using App.ECS;

namespace App.Code.Systems
{
    public class SpeedUISystem : ECS.System
    {
        private readonly Mediator _mediator;

        public override Type[] Filters { get; } =
            {typeof(MoveByInputComponent), typeof(SpeedComponent)};

        public SpeedUISystem(Mediator mediator) =>
            _mediator = mediator;

        protected override void Execute(Entity entity)
        {
            var speed = entity.GetComponent<SpeedComponent>();

            _mediator.UpdateSpeed(speed.Speed);
        }
    }
}