using UnityEngine;

namespace App.Code.Core.UI
{
    public class Mediator : MonoBehaviour
    {
        public StatisticView StatisticView;
        public ResultView ResultView;
        
        public void PlayerLoose(int score)
        {
            StatisticView.Hide();
            ResultView.Show();
            ResultView.ShowResults(score);
        }

        public void Restart()
        {
            StatisticView.ResetView();
            StatisticView.Show();
            ResultView.Hide();
        }

        public void UpdateCoordinates(Vector2 coordinates) => 
            StatisticView.UpdateCoordinates(coordinates);

        public void UpdateSpeed(float speed) => 
            StatisticView.UpdateSpeed(speed);

        public void UpdateScore(int score) => 
            StatisticView.UpdateScore(score);
    }
}