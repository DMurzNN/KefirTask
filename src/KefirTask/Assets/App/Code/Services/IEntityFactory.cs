using App.ECS;

namespace App.Code.Services
{
    public interface IEntityFactory
    {
        public Entity Create(string name);
    }
}