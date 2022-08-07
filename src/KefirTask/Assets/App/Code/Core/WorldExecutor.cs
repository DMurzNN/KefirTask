using App.Code.Components;
using App.Code.Services;
using App.Code.Systems;
using App.ECS;
using App.ECS.Prefab;
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
        public PrefabEntity Player;
        public PrefabEntity Bullet;
        public PrefabEntity Laser;
        public PrefabEntity EnemySpawner;
        public PrefabEntity AsteroidSpawner;

        private World _mainWorld;

        private IEntityFactory _entityFactory;
        private IScreenSizeService _screenSizeService;
        private ITimeService _timeService;
        private IInputService _inputService;
        private IWorldBoundsService _worldBoundsService;
        private IBulletFactory _bulletFactory;
        private ILaserFactory _laserFactory;
        private Entity _player;

        private void Awake() =>
            ConstructWorld();

        private void Update() =>
            _mainWorld.Run();

        private void ConstructWorld()
        {
            _mainWorld = new World();

            CreateServices();
            SetupServices();
            SetupEntities();
            SetupSystems();
        }

        private void CreateServices()
        {
            _entityFactory = new EntityFactory(_mainWorld);
            _screenSizeService = new ScreenSizeService();
            _timeService = new UnityTimeService();
            _inputService = new UnityInputService();
            _worldBoundsService = new WorldBoundsService(MainCamera, _screenSizeService);
            _bulletFactory = new BulletFactory(Bullet, _entityFactory);
            _laserFactory = new LaserFactory(Laser, _entityFactory);
        }

        private void SetupServices() =>
            _mainWorld
                .AddService(_timeService)
                .AddService(_inputService)
                .AddService(_screenSizeService)
                .AddService(_worldBoundsService);

        private void SetupEntities()
        {
            _player = _entityFactory.Create(Player);
            _entityFactory.Create(AsteroidSpawner);
            _entityFactory.Create(EnemySpawner)
                .GetComponent<EnemySpawnerComponent>().PlayerPosition = _player.GetComponent<PositionComponent>();
        }

        private void SetupSystems() =>
            _mainWorld
                .AddSystem(new LogSystem())
                .AddSystem(new MoveSystem(_worldBoundsService, _inputService, _timeService))
                .AddSystem(new RotateSystem(_timeService, _inputService))
                .AddSystem(new ForwardSystem())
                .AddSystem(new BulletShootSystem(_bulletFactory, _inputService))
                .AddSystem(new LaserShootSystem(_laserFactory, _inputService))
                .AddSystem(new LaserRefillSystem(_timeService))
                .AddSystem(new InfinityAccelerateSystem(_timeService, _worldBoundsService))
                .AddSystem(new FollowSystem(_timeService))
                .AddSystem(new EnemySpawnerSystem(_timeService, _entityFactory, _worldBoundsService))
                .AddSystem(new AsteroidSpawnerSystem(_timeService, _entityFactory, _worldBoundsService))
                .AddSystem(new LinkPositionSystem())
                .AddSystem(new LinkRotationSystem())
                .AddSystem(new LookAtForwardSystem())
                .AddSystem(new LifetimeSystem(_timeService))
                .AddSystem(new LinkToParentSystem())
                .AddSystem(new UpdateCapsuleColliderSystem())
                .AddSystem(new CollisionSystem())
                .AddSystem(new DestroySystem());
    }
}