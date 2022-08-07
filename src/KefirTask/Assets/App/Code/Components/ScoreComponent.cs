using System;
using App.ECS;

namespace App.Code.Components
{
    [Serializable]
    public class ScoreComponent : Component
    {
        public int Score;
    }
}