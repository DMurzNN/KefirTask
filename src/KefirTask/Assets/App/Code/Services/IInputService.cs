using App.ECS;

namespace App.Code.Services
{
    public interface IInputService : IService
    {
        public float Horizontal { get; }
        public float Vertical { get; }
        public bool ShootBullet { get; }
        public bool ShootLaser { get; }
    }
}