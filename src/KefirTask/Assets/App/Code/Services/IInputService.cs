namespace App.Code.Services
{
    public interface IInputService
    {
        public float Horizontal { get; }
        public float Vertical { get; }
        public bool ShootBullet { get; }
        public bool ShootLaser { get; }
    }
}