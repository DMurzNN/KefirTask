using System;
using App.Code.Components;
using App.Code.Services;
using App.ECS;

namespace App.Code.Systems
{
    public class MoveSystem : ECS.System
    {
        private readonly IWorldBoundsService _worldBoundsService;
        private readonly IInputService _inputService;
        private readonly ITimeService _timeService;

        public MoveSystem(IWorldBoundsService worldBoundsService, IInputService inputService, ITimeService timeService)
        {
            _inputService = inputService;
            _timeService = timeService;
            _worldBoundsService = worldBoundsService;
        }

        public override Type[] Filters => new[]
            {typeof(SpeedComponent), typeof(PositionComponent), typeof(DynamicComponent), typeof(ForwardComponent)};

        protected override void Execute(Entity entity)
        {
            var speed = entity.GetComponent<SpeedComponent>().Speed;
            var position = entity.GetComponent<PositionComponent>();
            var dynamic = entity.GetComponent<DynamicComponent>();
            var forward = entity.GetComponent<ForwardComponent>();

            var vertical = _inputService.Vertical;
            var worldBounds = _worldBoundsService.WorldBounds.To3D();
            if (vertical > 0.0f)
            {
                position.Position += forward.Forward * speed * _timeService.DeltaTime * vertical;
                position.Position = position.Position.Loop(-worldBounds, worldBounds);
            }
            dynamic.IsDynamic = vertical > 0.0f;
            if (vertical > 0.0f)
                dynamic.IsMoved = true;
        }
    }
}