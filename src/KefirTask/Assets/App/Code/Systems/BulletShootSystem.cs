using System;
using App.Code.Components;
using App.Code.Services;
using App.ECS;

namespace App.Code.Systems
{
    public class BulletShootSystem : ECS.System
    {
        private readonly IBulletFactory _bulletFactory;
        private readonly IInputService _inputService;

        public override Type[] Filters => new[]
            {typeof(BulletShootComponent), typeof(PositionComponent), typeof(ForwardComponent)};

        public BulletShootSystem(IBulletFactory bulletFactory, IInputService inputService)
        {
            _bulletFactory = bulletFactory;
            _inputService = inputService;
        }

        protected override void Execute(Entity entity)
        {
            var position = entity.GetComponent<PositionComponent>();
            var bulletShoot = entity.GetComponent<BulletShootComponent>();
            var forward = entity.GetComponent<ForwardComponent>();

            if (_inputService.ShootBullet)
                _bulletFactory.Create(position.Position, forward.Forward, bulletShoot.BulletAcceleration);
        }
    }
}