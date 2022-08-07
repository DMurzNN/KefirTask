using System;
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

        public static bool OutOf(this Vector3 value, Vector3 bounds, float delta)
        {
            bounds += Vector3.one * delta;
            return value.x > bounds.x || value.x < -bounds.x ||
                   value.y > bounds.y || value.y < -bounds.y ||
                   value.z > bounds.z || value.z < -bounds.z;
        }

        public static Vector3 MoveTowards(this Vector3 value, Vector3 target, float speed) =>
            Vector3.MoveTowards(value, target, speed);

        public static Vector3 Random(this Vector3 value) =>
            new()
            {
                x = UnityEngine.Random.Range(-value.x, value.x),
                y = UnityEngine.Random.Range(-value.y, value.y),
                z = UnityEngine.Random.Range(-value.z, value.z)
            };

        public static Vector3 Lerp(this Vector3 value, Vector3 target, float step) =>
            Vector3.Lerp(value, target, step);

        public static Vector2 RandomDirection(this Vector2 value) =>
            new Vector2(UnityEngine.Random.Range(-1.0f, 1.0f), UnityEngine.Random.Range(-1.0f, 1.0f)).normalized;

        public static float DistanceToLine(this Vector3 point, Vector3 lineStart, Vector3 lineEnd) =>
            Vector3.Magnitude(point.ProjectPointLine(lineStart, lineEnd) - point);

        public static Vector3 ProjectPointLine(this Vector3 point, Vector3 lineStart, Vector3 lineEnd)
        {
            var rhs = point - lineStart;
            var vector3 = lineEnd - lineStart;
            var magnitude = vector3.magnitude;
            var lhs = vector3;
            if (magnitude > 9.999999974752427E-07)
                lhs /= magnitude;
            var num = Mathf.Clamp(Vector3.Dot(lhs, rhs), 0.0f, magnitude);
            return lineStart + lhs * num;
        }
        
        public static Vector2 Random(this Vector2 value) =>
            new()
            {
                x = UnityEngine.Random.Range(-value.x, value.x),
                y = UnityEngine.Random.Range(-value.y, value.y)
            };
    }
}