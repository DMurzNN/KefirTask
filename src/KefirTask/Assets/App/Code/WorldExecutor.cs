using App.Code.Components;
using App.Code.Systems;
using App.ECS;
using Sirenix.OdinInspector;
using UnityEngine;

namespace App.Code
{
    public class WorldExecutor : MonoBehaviour
    {
#if UNITY_EDITOR
        [ShowInInspector] private World World => _mainWorld;
#endif
        private World _mainWorld;

        private void Awake() =>
            ConstructWorld();

        private void Update() =>
            _mainWorld.Run();

        private void ConstructWorld()
        {
            _mainWorld = new World();
            _mainWorld.AddEntity(LogEntity());
            _mainWorld.AddSystem(new LogSystem());
        }

        private static Entity LogEntity() =>
            new Entity()
                .With<LogComponent>();

#if UNITY_EDITOR
        [Button(ButtonStyle.FoldoutButton), DisableInEditorMode]
        private void AddMessage(string msg)
        {
            foreach (var e in _mainWorld.Entities)
            {
                var logComponent = e.GetComponent<LogComponent>();
                if (logComponent != null)
                    logComponent.Message = msg;
            }
        }
#endif
    }
}