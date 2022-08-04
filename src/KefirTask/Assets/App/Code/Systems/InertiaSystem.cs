using System;
using System.Linq;
using App.Code.Components;
using App.Code.Services;
using App.ECS;

namespace App.Code.Systems
{
    public class InertiaSystem : ECS.System
    {
        private readonly ITimeService _timeService;
        private IWorldBoundsService _worldBoundsService;

        public override Type[] Filters => new[]
        {
            typeof(InertiaComponent), typeof(DynamicComponent), typeof(PositionComponent), typeof(SpeedComponent),
            typeof(ForwardComponent)
        };

        public InertiaSystem(ITimeService timeService, IWorldBoundsService worldBoundsService)
        {
            _worldBoundsService = worldBoundsService;
            _timeService = timeService;
        }

        protected override void Execute(Entity entity)
        {
            var dynamic = entity.GetComponent<DynamicComponent>();
            var inertia = entity.GetComponent<InertiaComponent>();
            var position = entity.GetComponent<PositionComponent>();
            var speed = entity.GetComponent<SpeedComponent>();
            var forward = entity.GetComponent<ForwardComponent>();

            if (dynamic.IsDynamic || !dynamic.IsMoved)
                inertia.CurrentTime = 0.0f;
            else
            {
                var worldBounds = _worldBoundsService.WorldBounds.To3D();

                position.Position += forward.Forward * CalcDelta(inertia, speed.Speed);
                position.Position = position.Position.Loop(-worldBounds, worldBounds);

                if (inertia.CurrentTime <= inertia.InertiaCurve.keys.Last().time)
                    inertia.CurrentTime += _timeService.DeltaTime;
            }
        }

        private float CalcDelta(InertiaComponent inertia, float speed) =>
            speed * _timeService.DeltaTime * inertia.InertiaCurve.Evaluate(inertia.CurrentTime);
    }
}