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

        public override Type[] Filters => new[]
            {typeof(RotateComponent), typeof(RotateAccelerateComponent), typeof(RotateSpeedComponent)};

        public RotateSystem(ITimeService timeService, IInputService inputService)
        {
            _timeService = timeService;
            _inputService = inputService;
        }

        protected override void Execute(Entity entity)
        {
            var rotate = entity.GetComponent<RotateComponent>();
            var rotateAccelerate = entity.GetComponent<RotateAccelerateComponent>();
            var rotateSpeed = entity.GetComponent<RotateSpeedComponent>();

            var prevRotation = rotate.YAngle;

            if (_inputService.Horizontal == 0.0f && rotateSpeed.Speed.InRange(0.0f, 0.005f))
            {
                rotateSpeed.Speed = 0.0f;
                rotateAccelerate.AccelerationDirection = 0.0f;
                return;
            }

            var newDirection = CalcDirection(rotateAccelerate.AccelerationDirection, _inputService.Horizontal,
                rotateAccelerate.Accelerate, _inputService.Horizontal != 0.0, rotateAccelerate.Decelerate);
            var newRotation = prevRotation + newDirection;
            var newSpeed = (newRotation - prevRotation).Abs() / _timeService.PrevDeltaTime;
            
            rotateAccelerate.AccelerationDirection = newDirection;
            rotateSpeed.Speed = newSpeed;
            rotate.YAngle = newRotation;
            rotate.YAngle = rotate.YAngle.Loop(0.0f, 360.0f);
        }

        private float CalcDirection(float direction, float rotateSpeed,
            float acceleration, bool accelerate, float deceleration) =>
            accelerate
                ? direction + rotateSpeed * acceleration * _timeService.DeltaTime
                : direction - direction.Sign() * deceleration * _timeService.DeltaTime;
    }
}