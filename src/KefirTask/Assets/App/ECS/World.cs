using System;
using System.Collections.Generic;
using System.Linq;
using App.ECS.Components;
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
        private readonly HashSet<IService> _services;

        public World()
        {
            _systems = new HashSet<System>();
            _entities = new HashSet<Entity>();
            _services = new HashSet<IService>();
        }

        public void Run()
        {
            Clean();
            foreach (var service in _services)
                service.Update();

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

        public World AddService(IService service)
        {
            if (!_services.Contains(service))
                _services.Add(service);

            return this;
        }

        public void DestroyEntity(Entity entity)
        {
            if (!_entities.Contains(entity)) return;

            entity.AddComponent<DestroyComponent>();
        }

        private void ExecuteSystem(System system)
        {
            var filteredEntities = EntitiesForFilter(system.Filters);
            system.Execute(filteredEntities.ToArray());
        }

        private List<Entity> EntitiesForFilter(Type[] systemFilters)
        {
            var entityList = new List<Entity>();
            foreach (var entity in _entities)
                if (entity.ContainsComponents(systemFilters))
                    entityList.Add(entity);

            return entityList;
        }

        private void Clean()
        {
            _entities.RemoveWhere(e =>
                e.HasComponent<DestroyComponent>() && e.GetComponent<DestroyComponent>().Destroyed);
        }
    }
}