using App.ECS;
using UnityEngine;

namespace App.Code.Services
{
    public interface IInputService : IService
    {
        public Vector2 MainAxis { get; }
        public float Horizontal { get; }
        public float Vertical { get; }
        public bool ShootBullet { get; }
    }
}