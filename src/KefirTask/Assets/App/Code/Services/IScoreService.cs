using App.Code.Components;
using App.ECS;

namespace App.Code.Services
{
    public interface IScoreService : IService
    {
        public void RegisterPlayer(ScoreComponent playerScore);
        public void AddScore(int score);
    }
}