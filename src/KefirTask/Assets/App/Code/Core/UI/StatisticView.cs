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

        public void UpdateScore(int score) =>
            ScoreText.text = string.Format(ScoreFormat, score);

        public void UpdateCoordinates(Vector2 coordinates) =>
            CoordinatesText.text = string.Format(CoordinatesFormat, coordinates.x, coordinates.y);

        public void UpdateSpeed(float speed) =>
            SpeedText.text = string.Format(SpeedFormat, speed);

        public override void ResetView()
        {
            UpdateScore(0);
            UpdateCoordinates(Vector2.zero);
            UpdateSpeed(0.0f);
        }
    }
}