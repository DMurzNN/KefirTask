using System;
using App.Code.Components;
using App.Code.Core.UI;
using App.ECS;

namespace App.Code.Systems
{
    public class PlayerDestroySystem : ECS.System
    {
        public override Type[] Filters { get; } = {typeof(PlayerDestroyComponent), typeof(ScoreComponent)};
        
        private readonly Mediator _mediator;

        public PlayerDestroySystem(Mediator mediator) => 
            _mediator = mediator;

        protected override void Execute(Entity entity)
        {
            var score = entity.GetComponent<ScoreComponent>();

            entity.RemoveComponent<PlayerDestroyComponent>();
            
            _mediator.PlayerLoose(score.Score);
        }
    }
}