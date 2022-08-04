using System;
using UnityEngine;

namespace App.ECS.Components
{
    [Serializable]
    public class LinkComponent : Component
    {
        public GameObject LinkWith;
    }
}