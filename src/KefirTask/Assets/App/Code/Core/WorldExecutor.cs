using App.Code.Components;
using App.Code.Core.UI;
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
        public Mediator Mediator;
        public InputService InputService;
        
        public PrefabEntity Player;
        public PrefabEntity Bullet;
        public PrefabEntity Laser;
        public PrefabEntity EnemySpawner;
        public PrefabEntity AsteroidSpawner;

        private World _mainWorld;

        private IEntityFactory _entityFactory;
        private IScreenSizeService _screenSizeService;
        private ITimeService _timeService;
        private IWorldBoundsService _worldBoundsService;
        private IBulletFactory _bulletFactory;
        private ILaserFactory _laserFactory;
        private PieceFactory _pieceFactory;
        private ScoreService _scoreService;
        private Entity _player;

        private void Awake()
        {
            Mediator.Init();
            ConstructWorld();
        }

        private void Update() =>
            _mainWorld.Run();

        [Button(ButtonStyle.FoldoutButton), DisableInEditorMode]
        public void Restart()
        {
            _mainWorld.ClearWorld();
            SetupEntities();
        }

        public void Stop() => 
            _mainWorld.ClearWorld();

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
            _worldBoundsService = new WorldBoundsService(MainCamera, _screenSizeService);
            _bulletFactory = new BulletFactory(Bullet, _entityFactory);
            _laserFactory = new LaserFactory(Laser, _entityFactory);
            _pieceFactory = new PieceFactory(_entityFactory);
            _scoreService = new ScoreService(Mediator);
        }

        private void SetupServices() =>
            _mainWorld
                .AddService(_timeService)
                .AddService(_screenSizeService)
                .AddService(_worldBoundsService);

        private void SetupEntities()
        {
            _player = _entityFactory.Create(Player);
            _entityFactory.Create(AsteroidSpawner);
            _entityFactory.Create(EnemySpawner)
                .GetComponent<EnemySpawnerComponent>().PlayerPosition = _player.GetComponent<PositionComponent>();

            _scoreService.RegisterPlayer(_player.GetComponent<ScoreComponent>());
        }

        private void SetupSystems() =>
            _mainWorld
                .AddSystem(new LogSystem())
                .AddSystem(new MoveSystem(_worldBoundsService, InputService, _timeService))
                .AddSystem(new RotateSystem(_timeService, InputService))
                .AddSystem(new ForwardSystem())
                .AddSystem(new BulletShootSystem(_bulletFactory, InputService))
                .AddSystem(new LaserShootSystem(_laserFactory, InputService))
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
                .AddSystem(new CoordinateUISystem(Mediator))
                .AddSystem(new SpeedUISystem(Mediator))
                .AddSystem(new LaserUISystem(Mediator))
                .AddSystem(new AngleUISystem(Mediator))
                .AddSystem(new UpdateCapsuleColliderSystem())
                .AddSystem(new CollisionSystem())
                .AddSystem(new CrashSystem(_pieceFactory))
                .AddSystem(new DestroyByPlayerSystem(_scoreService))
                .AddSystem(new DestroySystem())
                .AddSystem(new PlayerDestroySystem(Mediator));
    }
}