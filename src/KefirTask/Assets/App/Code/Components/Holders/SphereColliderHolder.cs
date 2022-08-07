using UnityEngine;

namespace App.Code.Components.Holders
{
    public class SphereColliderHolder : ColliderHolder<SphereColliderComponent>
    {
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position + Component.Center, Component.Radius);
        }
    }
}