using TMPro;

namespace App.Code.Core.UI
{
    public class ResultView : View
    {
        public Mediator Mediator;
        
        public TMP_Text ScoreText;
        public string ScoreFormat;

        public void Restart() => 
            Mediator.Restart();
        
        public void ShowResults(int score) => 
            ScoreText.text = string.Format(ScoreFormat, score);

        public override void ResetView()
        {
            
        }
    }
}