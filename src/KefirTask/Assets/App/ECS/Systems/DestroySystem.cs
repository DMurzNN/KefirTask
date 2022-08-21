using System;
using App.ECS.Components;

namespace App.ECS.Systems
{
    public class DestroySystem : System
    {
        public override Type[] Filters { get; } = {typeof(DestroyComponent)};

        protected override void Execute(Entity entity)
        {
            if (entity.HasComponent<LinkComponent>())
                UnityEngine.Object.Destroy(entity.GetComponent<LinkComponent>().LinkWith);

            entity.GetComponent<DestroyComponent>().Destroyed = true;
        }
    }
}