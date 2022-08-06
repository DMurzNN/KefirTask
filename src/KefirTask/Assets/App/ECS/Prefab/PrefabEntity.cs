using UnityEngine;

namespace App.ECS.Prefab
{
    public class PrefabEntity : MonoBehaviour
    {
        public string Name;
        public ComponentHolder[] ComponentHolders;

        [ContextMenu("Collect All Holders")]
        private void CollectAllHolders() => 
            ComponentHolders = GetComponents<ComponentHolder>();
    }
}