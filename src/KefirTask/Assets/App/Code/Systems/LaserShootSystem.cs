using System;
using App.Code.Components;
using App.Code.Services;
using App.ECS;

namespace App.Code.Systems
{
    public class LaserShootSystem : ECS.System
    {
        private readonly ILaserFactory _laserFactory;
        private readonly IInputService _inputService;

        public override Type[] Filters => new[]
            {typeof(LaserShootComponent), typeof(PositionComponent), typeof(ForwardComponent)};

        public LaserShootSystem(ILaserFactory laserFactory, IInputService inputService)
        {
            _laserFactory = laserFactory;
            _inputService = inputService;
        }

        protected override void Execute(Entity entity)
        {
            var position = entity.GetComponent<PositionComponent>();
            var laserShoot = entity.GetComponent<LaserShootComponent>();
            var forward = entity.GetComponent<ForwardComponent>();

            if (!_inputService.ShootLaser || laserShoot.LaserCount <= 0) return;

            laserShoot.LaserCount--;
            _laserFactory.Create(position.Position, forward.Forward, entity);
        }
    }
}