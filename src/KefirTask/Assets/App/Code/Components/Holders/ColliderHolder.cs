using App.ECS;
using App.ECS.Prefab;

namespace App.Code.Components.Holders
{
    public class ColliderHolder<T> : ComponentHolder<T> 
        where T : ColliderComponent
    {
        public override Entity ApplyToEntity(Entity entity)
        {
            entity.AddComponent(Component as ColliderComponent);
            return entity;
        }
    }
}