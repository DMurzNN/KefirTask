using UnityEngine;

namespace App.Code.Services
{
    public class WorldBoundsService : IWorldBoundsService
    {
        public Vector2 WorldBounds { get; private set; }

        private readonly Camera _mainCamera;
        private readonly IScreenSizeService _screenSizeService;

        private Vector2 _screenSize;

        public WorldBoundsService(Camera mainCamera, IScreenSizeService screenSizeService)
        {
            _mainCamera = mainCamera;
            _screenSizeService = screenSizeService;
        }

        public void Update()
        {
            if (_screenSize != _screenSizeService.CurrentSize)
            {
                _screenSize = _screenSizeService.CurrentSize;
                UpdateBounds();
            }
        }

        private void UpdateBounds() =>
            WorldBounds = _mainCamera.ScreenToWorldPoint(_screenSize);
    }
}