using System;
using System.Collections.Generic;
using System.Linq;

namespace App.ECS
{
    [Serializable]
    public class Entity
    {
        public string Name { get; private set; }

        private readonly Dictionary<Type, Component> _components;

        public Entity(string name)
        {
            Name = name;
            _components = new Dictionary<Type, Component>();
        }

        public TComponent AddComponent<TComponent>(TComponent component) where TComponent : Component
        {
            if (_components.TryGetValue(typeof(TComponent), out var existComponent))
                return (TComponent) existComponent;

            _components.Add(typeof(TComponent), component);
            return component;
        }

        public TComponent AddComponent<TComponent>() where TComponent : Component, new()
        {
            if (_components.TryGetValue(typeof(TComponent), out var existComponent))
                return (TComponent) existComponent;

            var component = new TComponent();
            _components.Add(typeof(TComponent), component);
            return component;
        }

        public TComponent GetComponent<TComponent>() where TComponent : Component
        {
            if (_components.TryGetValue(typeof(TComponent), out var existComponent))
                return (TComponent) existComponent;

            return null;
        }

        public bool RemoveComponent<TComponent>() where TComponent : Component => 
            _components.Remove(typeof(TComponent));

        public bool HasComponent<T>() =>
            _components.ContainsKey(typeof(T));

        public bool ContainsComponents(Type[] components)
        {
            if (_components.Count < components.Length) return false;

            var componentCount = 0;
            foreach (var component in _components)
                if (components.Contains(component.Key))
                    componentCount++;

            return componentCount == components.Length;
        }

        public Entity With<TComponent>() where TComponent : Component, new()
        {
            AddComponent<TComponent>();
            return this;
        }

        public Entity With<TComponent>(TComponent component) where TComponent : Component
        {
            AddComponent(component);
            return this;
        }
    }
}