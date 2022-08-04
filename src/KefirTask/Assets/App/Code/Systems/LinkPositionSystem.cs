using System;
using App.Code.Components;
using App.ECS;

namespace App.Code.Systems
{
    public class LinkPositionSystem : ECS.System
    {
        public override Type[] Filters => new[] {typeof(LinkComponent), typeof(PositionComponent)};

        protected override void Execute(Entity entity) =>
            entity
                .GetComponent<LinkComponent>()
                .LinkWith.transform.position = entity.GetComponent<PositionComponent>().Position;
    }
}