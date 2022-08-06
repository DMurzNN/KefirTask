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

        public Entity Create(PrefabEntity prefabEntity)
        {
            var entityObject = Object.Instantiate(prefabEntity);
            var entity = Create(entityObject.Name);
            
            foreach(var holder in entityObject.ComponentHolders) 
                holder.ApplyToEntity(entity);
            
            return entity;
        }
    }
}