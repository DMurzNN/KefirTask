using App.ECS;
using UnityEngine;

namespace App.Code.Services
{
    public interface ILaserFactory
    {
        public Entity Create(Vector3 position, Vector3 direction, Entity parent);
    }
}