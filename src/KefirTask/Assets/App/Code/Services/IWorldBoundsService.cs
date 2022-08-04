using App.ECS;
using UnityEngine;

namespace App.Code.Services
{
    public interface IWorldBoundsService : IService
    {
        public Vector2 WorldBounds { get; }
    }
}