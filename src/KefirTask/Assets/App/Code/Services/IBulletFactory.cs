using App.ECS;
using UnityEngine;

namespace App.Code.Services
{
    public interface IBulletFactory
    {
        public Entity Create(Vector3 position, Vector3 direction, float acceleration);
    }
}