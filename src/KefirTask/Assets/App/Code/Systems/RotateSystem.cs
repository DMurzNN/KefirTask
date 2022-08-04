using System;
using App.Code.Components;
using App.Code.Services;
using App.ECS;

namespace App.Code.Systems
{
    public class RotateSystem : ECS.System
    {
        private readonly ITimeService _timeService;
        private readonly IInputService _inputService;
        public override Type[] Filters => new[] {typeof(RotateComponent), typeof(RotateSpeedComponent)};

        public RotateSystem(ITimeService timeService, IInputService inputService)
        {
            _timeService = timeService;
            _inputService = inputService;
        }

        protected override void Execute(Entity entity)
        {
            var rotate = entity.GetComponent<RotateComponent>();
            var rotateSpeed = entity.GetComponent<RotateSpeedComponent>();
            
            rotate.YAngle += rotateSpeed.Speed * _timeService.DeltaTime * _inputService.Horizontal;
            rotate.YAngle = rotate.YAngle.Loop(0.0f, 360.0f);
        }
    }
}