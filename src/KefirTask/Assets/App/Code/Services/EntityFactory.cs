using App.ECS;

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
    }
}