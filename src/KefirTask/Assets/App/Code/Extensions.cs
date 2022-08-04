using System;
using App.Code.Components;
using App.ECS;
using UnityEngine;

namespace App.Code
{
    public static class Extensions
    {
        public static T With<T>(this T self, Action<T> set)
        {
            set.Invoke(self);
            return self;
        }

        public static T With<T>(this T self, Action<T> apply, Func<bool> when)
        {
            if (when())
                apply?.Invoke(self);

            return self;
        }

        public static T With<T>(this T self, Action<T> apply, bool when)
        {
            if (when)
                apply?.Invoke(self);

            return self;
        }

        public static Entity LinkWith(this Entity entity, GameObject gameObject) =>
            entity
                .With(e => e.AddComponent<LinkComponent>().LinkWith = gameObject);
    }
}