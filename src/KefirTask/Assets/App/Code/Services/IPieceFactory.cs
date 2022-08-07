using App.ECS;
using App.ECS.Prefab;
using UnityEngine;

namespace App.Code.Services
{
    public interface IPieceFactory
    {
        public Entity Create(PrefabEntity prefab, Vector3 position);
    }
}