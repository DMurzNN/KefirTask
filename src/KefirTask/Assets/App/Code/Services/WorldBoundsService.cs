using UnityEngine;

namespace App.Code.Services
{
    public class WorldBoundsService : IWorldBoundsService
    {
        private const float RandomPositionBounds = 0.4f;

        public Vector2 WorldBounds { get; private set; }

        private readonly Camera _mainCamera;
        private readonly IScreenSizeService _screenSizeService;

        private Vector2 _screenSize;

        public WorldBoundsService(Camera mainCamera, IScreenSizeService screenSizeService)
        {
            _mainCamera = mainCamera;
            _screenSizeService = screenSizeService;
            
            Calculate();
        }

        public void Update()
        {
            if (_screenSize != _screenSizeService.CurrentSize) 
                Calculate();
        }

        public void Reset()
        {
        }

        public Vector3 RandomPosition()
        {
            var point = WorldBounds.Random();
            for (var i = 0; i < 2; i++)
            {
                var sign = point[i].Sign();
                var maxPos = WorldBounds[i].Abs() - RandomPositionBounds;
                if (point[i] < maxPos)
                    point[i] = maxPos * sign;
            }

            return point.To3D();
        }

        private void Calculate()
        {
            _screenSize = _screenSizeService.CurrentSize;
            UpdateBounds();
        }

        private void UpdateBounds() =>
            WorldBounds = _mainCamera.ScreenToWorldPoint(_screenSize).To2D();
    }
}