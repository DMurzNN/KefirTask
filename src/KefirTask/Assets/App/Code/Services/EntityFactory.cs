using App.Code.Components;
using App.ECS;
using App.ECS.Prefab;
using Object = UnityEngine.Object;

namespace App.Code.Services
{
    public class EntityFactory : IEntityFactory
    {
        private readonly World _world;

        public EntityFactory(World world) =>
            _world = world;

        public Entity Create(string name)
        {
            var e = new Entity(name);
            _world.AddEntity(e);
            return e;
        }

        public Entity Create(PrefabEntity prefabEntity, Entity parent = null)
        {
            var entityObject = Object.Instantiate(prefabEntity);
            var entity = Create(entityObject.Name);

            foreach (var holder in entityObject.ComponentHolders)
                holder.ApplyToEntity(entity);

            entity.With(e => e.AddComponent(new LinkToParentComponent
            {
                Parent = parent
            }), parent != null);

            return entity;
        }
    }
}