using System;
using App.Code.Core;
using App.ECS;

namespace App.Code.Components
{
    [Serializable]
    public class TagComponent : Component
    {
        public Tag Tag;
    }
}