using System;
using App.ECS;
using App.ECS.Prefab;

namespace App.Code.Components
{
    [Serializable]
    public class PieceComponent : Component
    {
        public int Count;
        public PrefabEntity PiecePrefab;
    }
}