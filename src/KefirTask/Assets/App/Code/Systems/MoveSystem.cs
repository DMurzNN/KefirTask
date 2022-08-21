using System;
using App.Code.Components;
using App.Code.Services;
using App.ECS;
using UnityEngine;

namespace App.Code.Systems
{
    public class MoveSystem : ECS.System
    {
        private readonly IWorldBoundsService _worldBoundsService;
        private readonly IInputService _inputService;
        private readonly ITimeService _timeService;

        public override Type[] Filters { get; } =
        {
            typeof(SpeedComponent), typeof(PositionComponent), typeof(ForwardComponent), typeof(DecelerationComponent),
            typeof(AccelerationComponent), typeof(MoveByInputComponent)
        };

        public MoveSystem(IWorldBoundsService worldBoundsService, IInputService inputService, ITimeService timeService)
        {
            _inputService = inputService;
            _timeService = timeService;
            _worldBoundsService = worldBoundsService;
        }

        protected override void Execute(Entity entity)
        {
            var speed = entity.GetComponent<SpeedComponent>();
            var position = entity.GetComponent<PositionComponent>();
            var forward = entity.GetComponent<ForwardComponent>();
            var acceleration = entity.GetComponent<AccelerationComponent>();
            var deceleration = entity.GetComponent<DecelerationComponent>();

            var vertical = _inputService.Vertical;
            var worldBounds = _worldBoundsService.WorldBounds.To3D();
            var moveSpeed = vertical == 0 ? -1.0f : vertical;

            var newAccelerationDirection = CalcDirection(acceleration.AccelerationDirection, forward.Forward, moveSpeed,
                acceleration.Acceleration, moveSpeed >= 0, deceleration.Deceleration);
            var newPosition = position.Position + newAccelerationDirection;
            var newSpeed = (newPosition - position.Position).magnitude / _timeService.DeltaTime;

            if (vertical <= 0.0f && speed.Speed.InRange(0.0f, 0.005f))
            {
                speed.Speed = 0.0f;
                acceleration.AccelerationDirection = Vector3.zero;
                return;
            }

            acceleration.AccelerationDirection = newAccelerationDirection;
            speed.Speed = newSpeed;

            position.Position = newPosition;
            position.Position = position.Position.Loop(-worldBounds, worldBounds);
        }

        private Vector3 CalcDirection(Vector3 direction, Vector3 forward, float moveSpeed,
            float acceleration, bool accelerate, float deceleration) =>
            accelerate
                ? direction + forward * moveSpeed * acceleration * _timeService.DeltaTime
                : direction + direction * moveSpeed * deceleration * _timeService.DeltaTime;
    }
}