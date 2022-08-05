using App.Code.Adapters;
using App.Code.Components;
using App.Code.Services;
using App.Code.Systems;
using App.ECS;
using App.ECS.Systems;
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
        public Bullet Bullet;

        private World _mainWorld;
        private IEntityFactory _entityFactory;

        private void Awake() =>
            ConstructWorld();

        private void Update() =>
            _mainWorld.Run();

        private void ConstructWorld()
        {
            _mainWorld = new World();

            _entityFactory = new EntityFactory(_mainWorld);

            IScreenSizeService screenSizeService = new ScreenSizeService();
            ITimeService timeService = new UnityTimeService();
            IInputService inputService = new UnityInputService();
            IWorldBoundsService worldBoundsService = new WorldBoundsService(MainCamera, screenSizeService);
            IBulletFactory bulletFactory = new BulletFactory(Bullet, _entityFactory);

            _mainWorld
                .AddService(timeService)
                .AddService(inputService)
                .AddService(screenSizeService)
                .AddService(worldBoundsService);

            _mainWorld
                .AddEntity(new Entity("Log")
                    .With<LogComponent>())
                .AddEntity(new Entity("Ship")
                    .With<AccelerationComponent>()
                    .With<ForwardComponent>()
                    .With<PositionComponent>()
                    .With<SpeedComponent>()
                    .With<RotateComponent>()
                    .With<RotateSpeedComponent>()
                    .With<RotateAccelerateComponent>()
                    .With<BulletShootComponent>()
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
                .AddSystem(new BulletShootSystem(bulletFactory, inputService))
                .AddSystem(new InfinityAccelerateSystem(timeService, worldBoundsService))
                .AddSystem(new LinkPositionSystem())
                .AddSystem(new LinkRotationSystem())
                .AddSystem(new CollisionSystem())
                .AddSystem(new DestroySystem());
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