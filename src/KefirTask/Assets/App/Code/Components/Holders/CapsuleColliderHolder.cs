using UnityEngine;

namespace App.Code.Components.Holders
{
    public class CapsuleColliderHolder : ColliderHolder<CapsuleColliderComponent>
    {
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position + Component.Center + Component.MaxPoint, Component.Radius);
            Gizmos.DrawWireSphere(transform.position + Component.Center + Component.MinPoint, Component.Radius);
        }
    }
}