using System;
using App.Code.Components;
using App.ECS;
using App.ECS.Components;

namespace App.Code.Systems
{
    public class LinkRotationSystem : ECS.System
    {
        public override Type[] Filters { get; } =
            {typeof(LinkComponent), typeof(RotateComponent)};

        protected override void Execute(Entity entity)
        {
            var link = entity.GetComponent<LinkComponent>();
            var rotate = entity.GetComponent<RotateComponent>();

            var transform = link.LinkWith.transform;

            var rotationObject = transform.localEulerAngles;
            rotationObject.y = rotate.YAngle;
            transform.localEulerAngles = rotationObject;
        }
    }
}