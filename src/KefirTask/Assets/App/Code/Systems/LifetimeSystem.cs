using System;
using App.Code.Components;
using App.Code.Services;
using App.ECS;
using App.ECS.Components;

namespace App.Code.Systems
{
    public class LifetimeSystem : ECS.System
    {
        private readonly ITimeService _timeService;
        public override Type[] Filters { get; } = {typeof(LifetimeComponent)};

        public LifetimeSystem(ITimeService timeService) =>
            _timeService = timeService;

        protected override void Execute(Entity entity)
        {
            var lifetime = entity.GetComponent<LifetimeComponent>();

            if (lifetime.CurrentLifetime >= lifetime.Lifetime)
                entity.AddComponent<DestroyComponent>();
            else
                lifetime.CurrentLifetime += _timeService.DeltaTime;
        }
    }
}