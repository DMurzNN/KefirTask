using System;
using App.Code.Components;
using App.Code.Services;
using App.ECS;

namespace App.Code.Systems
{
    public class FollowSystem : ECS.System
    {
        private readonly ITimeService _timeService;

        public override Type[] Filters => new[] {typeof(FollowComponent), typeof(PositionComponent)};

        public FollowSystem(ITimeService timeService) => 
            _timeService = timeService;

        protected override void Execute(Entity entity)
        {
            var follow = entity.GetComponent<FollowComponent>();
            var position = entity.GetComponent<PositionComponent>();

            position.Position = position.Position.MoveTowards(follow.TargetPosition.Position,
                follow.Speed * _timeService.DeltaTime);
        }
    }
}