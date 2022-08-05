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
        public AnimationCurve TestInertiaCurve;

        private World _mainWorld;

        private void Awake() =>
            ConstructWorld();

        private void Update() =>
            _mainWorld.Run();

        private void ConstructWorld()
        {
            _mainWorld = new World();

            IScreenSizeService screenSizeService = new ScreenSizeService();
            ITimeService timeService = new UnityTimeService();
            IInputService inputService = new UnityInputService();
            IWorldBoundsService worldBoundsService = new WorldBoundsService(MainCamera, screenSizeService);
            
            _mainWorld
                .AddService(timeService)
                .AddService(inputService)
                .AddService(screenSizeService)
                .AddService(worldBoundsService);

            _mainWorld
                .AddEntity(new Entity()
                    .With<LogComponent>())
                .AddEntity(new Entity()
                    .With<AccelerationComponent>()
                    .With<ForwardComponent>()
                    .With<PositionComponent>()
                    .With<SpeedComponent>()
                    .With<RotateComponent>()
                    .With<RotateSpeedComponent>()
                    .With<RotateAccelerateComponent>()
                    .With(new CollisionComponent
                    {
                        CollisionAdapter = TestAdapter
                    })
                    .LinkWith(TestLink));

            _mainWorld
                .AddSystem(new LogSystem())
                .AddSystem(new MoveSystem(worldBoundsService, inputService, timeService))
                .AddSystem(new RotateSystem(timeService, inputService))
                .AddSystem(new ForwardSystem())
                .AddSystem(new LinkPositionSystem())
                .AddSystem(new LinkRotationSystem())
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