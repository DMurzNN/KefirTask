using TMPro;
using UnityEngine;

namespace App.Code.Core.UI
{
    public class StatisticView : View
    {
        public TMP_Text ScoreText;
        public string ScoreFormat;

        public TMP_Text CoordinatesText;
        public string CoordinatesFormat;

        public TMP_Text SpeedText;
        public string SpeedFormat;
        
        public TMP_Text AngleText;
        public string AngleFormat;
        
        public TMP_Text LaserCountText;
        public string LaserCountFormat;
        
        public TMP_Text LaserCooldownText;
        public string LaserCooldownFormat;

        private Vector2 _prevCoordinates;
        private float _prevSpeed;
        private int _prevScore;
        private float _prevAngle;
        private int _prevLaserCount;
        private float _prevLaserCooldown;

        public void UpdateScore(int score)
        {
            if (_prevScore == score) return;

            _prevScore = score;
            ScoreText.text = string.Format(ScoreFormat, score);
        }

        public void UpdateCoordinates(Vector2 coordinates)
        {
            if (_prevCoordinates == coordinates) return;

            _prevCoordinates = coordinates;
            CoordinatesText.text = string.Format(CoordinatesFormat, coordinates.x, coordinates.y);
        }

        public void UpdateSpeed(float speed)
        {
            if (_prevSpeed.Equals(speed)) return;

            _prevSpeed = speed;
            SpeedText.text = string.Format(SpeedFormat, speed);
        }
        
        public void UpdateAngle(float angle)
        {
            if (_prevAngle.Equals(angle)) return;

            _prevAngle = angle;
            AngleText.text = string.Format(AngleFormat, angle);
        }
        
        public void UpdateLaserCount(int laserCount)
        {
            if (_prevLaserCount.Equals(laserCount)) return;

            _prevLaserCount = laserCount;
            LaserCountText.text = string.Format(LaserCountFormat, laserCount);
        }
        
        public void UpdateLaserCooldown(float laserCooldown, float totalRefillTime)
        {
            if (_prevLaserCooldown.Equals(laserCooldown)) return;

            _prevLaserCooldown = laserCooldown;
            LaserCooldownText.text = string.Format(LaserCooldownFormat, laserCooldown, totalRefillTime);
        }

        public override void ResetView()
        {
            _prevCoordinates = Vector2.positiveInfinity;
            _prevSpeed = float.MaxValue;
            _prevScore = int.MaxValue;
            _prevAngle = float.MaxValue;
            _prevLaserCooldown = float.MaxValue;
            _prevLaserCount = int.MaxValue;

            UpdateScore(0);
            UpdateCoordinates(Vector2.zero);
            UpdateSpeed(0.0f);
            UpdateAngle(0.0f);
            UpdateLaserCooldown(0.0f, 0.0f);
            UpdateLaserCount(0);
        }
    }
}