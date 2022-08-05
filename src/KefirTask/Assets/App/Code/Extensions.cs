﻿using System;
using App.Code.Components;
using App.ECS;
using App.ECS.Components;
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

        public static float Loop(this float value, float minValue, float maxValue)
        {
            if (value < minValue) return maxValue;
            
            if (value > maxValue) return minValue;
            
            return value;
        }
        
        public static Vector2 Loop(this Vector2 value, Vector2 minValue, Vector2 maxValue)
        {
            value.x = value.x.Loop(minValue.x, maxValue.x);
            value.y = value.y.Loop(minValue.y, maxValue.y);
            return value;
        }
        
        public static Vector3 Loop(this Vector3 value, Vector3 minValue, Vector3 maxValue)
        {
            value.x = value.x.Loop(minValue.x, maxValue.x);
            value.y = value.y.Loop(minValue.y, maxValue.y);
            value.z = value.z.Loop(minValue.z, maxValue.z);
            return value;
        }

        public static Vector3 To3D(this Vector2 vector) => 
            new(vector.x, 0, vector.y);
        
        public static Vector2 To2D(this Vector3 vector) => 
            new(vector.x, vector.z);

        public static float ToRadians(this float degrees) => 
            degrees * (Mathf.PI / 180.0f);

        public static bool InRange(this float value, Vector2 delta) =>
            value >= delta.x && value <= delta.y;

        public static bool InRange(this float value, float targetValue, float delta) =>
            value.InRange(new Vector2(targetValue - delta, targetValue + delta));

        public static float Sign(this float value) =>
            value switch
            {
                > 0.0f => 1.0f,
                < 0.0f => -1.0f,
                _ => 0.0f
            };

        public static float Abs(this float value) =>
            Mathf.Abs(value);
    }
}