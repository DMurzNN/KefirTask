using System;
using App.Code.Components;
using App.ECS;
using App.ECS.Components;

namespace App.Code.Systems
{
    public class LookAtForwardSystem : ECS.System
    {
        public override Type[] Filters { get; } =
            {typeof(LinkComponent), typeof(LookAtForwardComponent), typeof(ForwardComponent)};

        protected override void Execute(Entity entity)
        {
            var link = entity.GetComponent<LinkComponent>();
            var forward = entity.GetComponent<ForwardComponent>();

            link.LinkWith.transform.forward = forward.Forward;
        }
    }
}