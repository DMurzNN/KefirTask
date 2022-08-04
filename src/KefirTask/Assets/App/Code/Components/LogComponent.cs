using System;
using App.ECS;

namespace App.Code.Components
{
    [Serializable]
    public class LogComponent : Component
    {
        public string Message;
    }
}