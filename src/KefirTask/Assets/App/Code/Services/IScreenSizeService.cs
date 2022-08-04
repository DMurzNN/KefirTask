using App.ECS;
using UnityEngine;

namespace App.Code.Services
{
    public interface IScreenSizeService : IService
    {
        public Vector2 CurrentSize { get; }
        public float ScreenRatio();
    }
}