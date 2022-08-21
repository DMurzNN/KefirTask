using System;
using App.Code.Components;
using App.Code.Services;
using App.ECS;

namespace App.Code.Systems
{
    public class LaserRefillSystem : ECS.System
    {
        private readonly ITimeService _timeService;

        public override Type[] Filters { get; } = {typeof(LaserShootComponent)};

        public LaserRefillSystem(ITimeService timeService) =>
            _timeService = timeService;

        protected override void Execute(Entity entity)
        {
            var laser = entity.GetComponent<LaserShootComponent>();

            if (laser.RefillTimer >= laser.LaserRefillTime)
            {
                laser.LaserCount++;
                laser.RefillTimer = 0.0f;
            }
            else
                laser.RefillTimer += _timeService.DeltaTime;
        }
    }
}