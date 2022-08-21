using System;
using App.Code.Components;
using App.Code.Services;
using App.ECS;
using App.ECS.Components;

namespace App.Code.Systems
{
    public class DestroyByPlayerSystem : ECS.System
    {
        private const int ScoreForKill = 1;

        private readonly IScoreService _scoreService;

        public override Type[] Filters { get; } = {typeof(DestroyByPlayerComponent)};

        public DestroyByPlayerSystem(IScoreService scoreService) =>
            _scoreService = scoreService;

        protected override void Execute(Entity entity)
        {
            _scoreService.AddScore(ScoreForKill);
            entity.AddComponent<DestroyComponent>();
        }
    }
}