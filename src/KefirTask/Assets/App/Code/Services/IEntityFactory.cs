using App.ECS;
using App.ECS.Prefab;

namespace App.Code.Services
{
    public interface IEntityFactory
    {
        public Entity Create(string name);
        public Entity Create(PrefabEntity prefabEntity, Entity parent = null);
    }
}