using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;

namespace App.ECS
{
    [Serializable]
    public class World
    {
#if UNITY_EDITOR
        [ShowInInspector] public Entity[] Entities => _entities.ToArray();
#endif
        
        private readonly HashSet<System> _systems;
        private readonly HashSet<Entity> _entities;

        public World()
        {
            _systems = new HashSet<System>();
            _entities = new HashSet<Entity>();
        }

        public void Run()
        {
            foreach (var system in _systems)
                ExecuteSystem(system);
        }

        public World AddSystem(System system)
        {
            if (!_systems.Contains(system))
                _systems.Add(system);

            return this;
        }

        public World AddEntity(Entity entity)
        {
            if (!_entities.Contains(entity))
                _entities.Add(entity);

            return this;
        }

        private void ExecuteSystem(System system)
        {
            var filteredEntities = EntitiesForFilter(system.Filters);
            system.Execute(filteredEntities.ToArray());
        }

        private IEnumerable<Entity> EntitiesForFilter(Type[] systemFilters)
        {
            var entityList = new List<Entity>();
            foreach (var entity in _entities)
                if (entity.ContainsComponents(systemFilters))
                    entityList.Add(entity);

            return entityList;
        }
    }
}