using UnityEngine;

namespace App.Code.Adapters
{
    public class CollisionAdapter : MonoBehaviour
    {
        public bool IsCollide { get; private set; }

        private void OnCollisionEnter(Collision collision) =>
            IsCollide = true;

        private void OnCollisionExit(Collision other) =>
            IsCollide = false;
    }
}