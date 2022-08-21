using System;
using App.Code.Components;
using App.Code.Core.UI;
using App.ECS;

namespace App.Code.Systems
{
    public class AngleUISystem : ECS.System
    {
        public override Type[] Filters { get; } = {typeof(RotateComponent), typeof(MoveByInputComponent)};
        
        private readonly Mediator _mediator;

        public AngleUISystem(Mediator mediator) => 
            _mediator = mediator;

        protected override void Execute(Entity entity)
        {
            var rotate = entity.GetComponent<RotateComponent>();
            
            _mediator.UpdateAngle(rotate.YAngle);
        }
    }
}