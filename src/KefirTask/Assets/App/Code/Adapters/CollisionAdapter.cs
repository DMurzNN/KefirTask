using System.Collections.Generic;
using UnityEngine;

namespace App.Code.Adapters
{
    public class CollisionAdapter : MonoBehaviour
    {
        public bool IsCollide => _colliders.Count > 0;

        private HashSet<Collider> _colliders;

        private void Awake() =>
            _colliders = new HashSet<Collider>();

        private void OnCollisionEnter(Collision collision)
        {
            if (_colliders.Contains(collision.collider)) return;

            _colliders.Add(collision.collider);
        }

        private void OnCollisionExit(Collision collision)
        {
            if (!_colliders.Contains(collision.collider)) return;

            _colliders.Remove(collision.collider);
        }
    }
}