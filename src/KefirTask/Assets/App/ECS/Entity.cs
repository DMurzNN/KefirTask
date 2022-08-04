using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;

namespace App.ECS
{
    [Serializable]
    public class Entity
    {
#if UNITY_EDITOR
        [ShowInInspector] private Component[] Components => _components.Values.ToArray();
#endif
        private readonly Dictionary<Type, Component> _components;

        public Entity() =>
            _components = new Dictionary<Type, Component>();

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

        public bool ContainsComponents(Type[] components)
        {
            if (_components.Count < components.Length) return false;

            foreach (var component in _components)
                if (!components.Contains(component.Value.GetType()))
                    return false;

            return true;
        }

        public Entity With<TComponent>() where TComponent : Component, new()
        {
            AddComponent<TComponent>();
            return this;
        }
    }
}