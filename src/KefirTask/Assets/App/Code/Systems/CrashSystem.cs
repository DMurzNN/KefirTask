using System;
using App.Code.Components;
using App.Code.Services;
using App.ECS;
using App.ECS.Components;

namespace App.Code.Systems
{
    public class CrashSystem : ECS.System
    {
        private readonly IPieceFactory _pieceFactory;

        public override Type[] Filters => new[]
            {typeof(PositionComponent), typeof(CrashComponent), typeof(PieceComponent)};

        public CrashSystem(IPieceFactory pieceFactory) =>
            _pieceFactory = pieceFactory;

        protected override void Execute(Entity entity)
        {
            var position = entity.GetComponent<PositionComponent>();
            var piece = entity.GetComponent<PieceComponent>();

            entity.AddComponent<DestroyComponent>();
            for (var i = 0; i < piece.Count; i++)
                _pieceFactory.Create(piece.PiecePrefab, position.Position);
        }
    }
}