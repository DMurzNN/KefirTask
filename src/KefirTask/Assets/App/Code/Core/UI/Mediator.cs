using UnityEngine;

namespace App.Code.Core.UI
{
    public class Mediator : MonoBehaviour
    {
        public WorldExecutor WorldExecutor;
        public StatisticView StatisticView;
        public ResultView ResultView;

        public void PlayerLoose(int score)
        {
            StatisticView.Hide();
            ResultView.Show();
            ResultView.ShowResults(score);
            WorldExecutor.Stop();
        }

        public void Init()
        {
            StatisticView.ResetView();
            StatisticView.Show();
            ResultView.Hide();
        }
        
        public void Restart()
        {
            Init();
            WorldExecutor.Restart();
        }

        public void UpdateCoordinates(Vector2 coordinates) =>
            StatisticView.UpdateCoordinates(coordinates);

        public void UpdateSpeed(float speed) =>
            StatisticView.UpdateSpeed(speed);

        public void UpdateScore(int score) =>
            StatisticView.UpdateScore(score);

        public void UpdateAngle(float angle) =>
            StatisticView.UpdateAngle(angle);

        public void UpdateLaserCount(int laserCount) =>
            StatisticView.UpdateLaserCount(laserCount);

        public void UpdateLaserCooldown(float laserCooldown, float totalRefillTime) =>
            StatisticView.UpdateLaserCooldown(laserCooldown, totalRefillTime);
    }
}