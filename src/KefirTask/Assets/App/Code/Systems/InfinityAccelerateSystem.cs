using System;
using App.Code.Components;
using App.Code.Services;
using App.ECS;
using App.ECS.Components;

namespace App.Code.Systems
{
    public class InfinityAccelerateSystem : ECS.System
    {
        private readonly ITimeService _timeService;
        private readonly IWorldBoundsService _worldBoundsService;

        public override Type[] Filters => new[]
            {typeof(PositionComponent), typeof(InfinityAccelerationComponent), typeof(ForwardComponent)};

        public InfinityAccelerateSystem(ITimeService timeService, IWorldBoundsService worldBoundsService)
        {
            _timeService = timeService;
            _worldBoundsService = worldBoundsService;
        }

        protected override void Execute(Entity entity)
        {
            var position = entity.GetComponent<PositionComponent>();
            var accelerate = entity.GetComponent<InfinityAccelerationComponent>();
            var forward = entity.GetComponent<ForwardComponent>();

            accelerate.AccelerationDirection += forward.Forward * accelerate.Acceleration * _timeService.DeltaTime;
            position.Position += accelerate.AccelerationDirection;

            if (position.Position.OutOf(_worldBoundsService.WorldBounds, 10.0f))
                entity.AddComponent<DestroyComponent>();
        }
    }
}