using System;
using App.Code.Components;
using App.ECS;
using UnityEngine;

namespace App.Code.Systems
{
    public class LogSystem : ECS.System
    {
        public override Type[] Filters => new[] {typeof(LogComponent)};

        protected override void Execute(Entity entity)
        {
            var logComponent = entity.GetComponent<LogComponent>();
            if (!string.IsNullOrEmpty(logComponent.Message))
            {
                Debug.Log(logComponent.Message);
                logComponent.Message = null;
            }
        }
    }
}