using System;
using App.ECS;

namespace App.Code.Components
{
    [Serializable]
    public class LinkToParentComponent : Component
    {
        public Entity Parent;
    }
}