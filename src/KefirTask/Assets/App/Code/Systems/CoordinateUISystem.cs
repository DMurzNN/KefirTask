using System;
using App.Code.Components;
using App.Code.Core.UI;
using App.ECS;

namespace App.Code.Systems
{
    public class CoordinateUISystem : ECS.System
    {
        private readonly Mediator _mediator;

        public override Type[] Filters { get; } =
            {typeof(PositionComponent), typeof(MoveByInputComponent)};

        public CoordinateUISystem(Mediator mediator) => 
            _mediator = mediator;

        protected override void Execute(Entity entity)
        {
            var position = entity.GetComponent<PositionComponent>();
            
            _mediator.UpdateCoordinates(position.Position.To2D());
        }
    }
}