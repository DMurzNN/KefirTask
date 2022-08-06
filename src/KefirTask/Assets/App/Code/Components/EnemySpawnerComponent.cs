using System;

namespace App.Code.Components
{
    [Serializable]
    public class EnemySpawnerComponent : SpawnerComponent
    {
        public PositionComponent PlayerPosition;
    }
}