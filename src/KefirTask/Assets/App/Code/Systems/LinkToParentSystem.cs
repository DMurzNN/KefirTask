using System;
using App.Code.Components;
using App.ECS;

namespace App.Code.Systems
{
    public class LinkToParentSystem : ECS.System
    {
        public override Type[] Filters => new[] {typeof(LinkToParentComponent)};

        protected override void Execute(Entity entity)
        {
            var link = entity.GetComponent<LinkToParentComponent>();
            var parent = link.Parent;

            entity
                .With(e => e
                        .GetComponent<ForwardComponent>().Forward = parent.GetComponent<ForwardComponent>().Forward,
                    entity.HasComponent<ForwardComponent>() && parent.HasComponent<ForwardComponent>())
                .With(e => e
                        .GetComponent<PositionComponent>().Position = parent.GetComponent<PositionComponent>().Position,
                    entity.HasComponent<PositionComponent>() && parent.HasComponent<PositionComponent>());
        }
    }
}