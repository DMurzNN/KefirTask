using App.Code.Adapters;
using App.Code.Components;
using App.Code.Services;
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
        public Camera MainCamera;
        public GameObject TestLink;
        public CollisionAdapter TestAdapter;

        private World _mainWorld;

        private void Awake() =>
            ConstructWorld();

        private void Update() =>
            _mainWorld.Run();

        private void ConstructWorld()
        {
            _mainWorld = new World();

            IScreenSizeService screenSizeService = new ScreenSizeService();
            var worldBoundsService = new WorldBoundsService(MainCamera, screenSizeService);
            _mainWorld
                .AddService(screenSizeService)
                .AddService(worldBoundsService);

            _mainWorld
                .AddEntity(new Entity()
                    .With<LogComponent>())
                .AddEntity(new Entity()
                    .With<PositionComponent>()
                    .With<SpeedComponent>()
                    .With(new CollisionComponent
                    {
                        CollisionAdapter = TestAdapter
                    })
                    .LinkWith(TestLink));

            _mainWorld
                .AddSystem(new LogSystem())
                .AddSystem(new MoveSystem(worldBoundsService))
                .AddSystem(new LinkPositionSystem())
                .AddSystem(new CollisionSystem());
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