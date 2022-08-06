using UnityEngine;

namespace App.ECS.Prefab
{
    public class ComponentHolder<T> : ComponentHolder 
        where T : Component
    {
        public T Component;

        public override Component GetComponent() => 
            Component;

        public override Entity ApplyToEntity(Entity entity)
        {
            entity.AddComponent(Component);
            return entity;
        }
    }

    public abstract class ComponentHolder : MonoBehaviour
    {
        public abstract Component GetComponent();
        public abstract Entity ApplyToEntity(Entity entity);
    }
}