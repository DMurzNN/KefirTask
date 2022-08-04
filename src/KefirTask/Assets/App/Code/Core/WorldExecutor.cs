using App.Code.Components;
using App.Code.Systems;
using App.ECS;
using Sirenix.OdinInspector;
using UnityEngine;

namespace App.Code.Core
{
    public class WorldExecutor : MonoBehaviour
    {
#if UNITY_EDITOR
        [ShowInInspector] private World World => _mainWorld;
#endif
        public GameObject TestLink;
        
        private World _mainWorld;

        private void Awake() =>
            ConstructWorld();

        private void Update() =>
            _mainWorld.Run();

        private void ConstructWorld()
        {
            _mainWorld = new World();

            _mainWorld
                .AddEntity(new Entity()
                    .With<LogComponent>())
                .AddEntity(new Entity()
                    .With<PositionComponent>()
                    .With<SpeedComponent>()
                    .LinkWith(TestLink));

            _mainWorld
                .AddSystem(new LogSystem())
                .AddSystem(new MoveSystem())
                .AddSystem(new LinkPositionSystem());
        }

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