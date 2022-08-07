using UnityEngine;

namespace App.Code.Services
{
    public class ScreenSizeService : IScreenSizeService
    {
        public Vector2 CurrentSize
        {
            get
            {
                if (_currentSize == Vector2.zero)
                    CurrentSize = CurrentScreenSize();
                return _currentSize;
            }
            private set => _currentSize = value;
        }

        private Vector2 _currentSize;

        private static Vector2 CurrentScreenSize() =>
            new(Screen.width, Screen.height);

        private Vector2 InverseCurrentScreenSize() =>
            new(CurrentSize.y, CurrentSize.x);

        public void Update()
        {
            var currentScreenSize = CurrentScreenSize();
            var currentInverseScreenSize = InverseCurrentScreenSize();
            if (currentScreenSize != CurrentSize && currentScreenSize != currentInverseScreenSize)
                CurrentSize = currentScreenSize;
        }

        public void Reset()
        {
        }
    }
}