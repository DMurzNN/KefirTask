using App.Code.Components;
using App.Code.Core.UI;

namespace App.Code.Services
{
    public class ScoreService : IScoreService
    {
        private readonly Mediator _mediator;
        private ScoreComponent _playerScore;

        public ScoreService(Mediator mediator) => 
            _mediator = mediator;

        public void RegisterPlayer(ScoreComponent playerScore) =>
            _playerScore = playerScore;

        public void AddScore(int score)
        {
            _playerScore.Score += score;
            _mediator.UpdateScore(_playerScore.Score);
        }

        public void Update()
        {
        }

        public void Reset()
        {
        }
    }
}