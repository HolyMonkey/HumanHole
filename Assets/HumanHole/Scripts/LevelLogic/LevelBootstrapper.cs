using System.Threading.Tasks;
using HumanHole.Scripts.Camera;
using HumanHole.Scripts.Data;
using HumanHole.Scripts.Gesture;
using HumanHole.Scripts.Handlers;
using HumanHole.Scripts.Infrastructure;
using HumanHole.Scripts.Infrastructure.Services;
using HumanHole.Scripts.Infrastructure.Services.Ads;
using HumanHole.Scripts.Infrastructure.Services.Analytics;
using HumanHole.Scripts.Infrastructure.Services.AssetsManagement;
using HumanHole.Scripts.Infrastructure.Services.Authorization;
using HumanHole.Scripts.Infrastructure.Services.Download;
using HumanHole.Scripts.Infrastructure.Services.Factory;
using HumanHole.Scripts.Infrastructure.Services.LeaderBoard;
using HumanHole.Scripts.Infrastructure.Services.PersistentProgress;
using HumanHole.Scripts.Infrastructure.Services.Profile;
using HumanHole.Scripts.Infrastructure.Services.SaveLoad;
using HumanHole.Scripts.Infrastructure.States;
using HumanHole.Scripts.Level;
using HumanHole.Scripts.Player;
using HumanHole.Scripts.UI;
using HumanHole.Scripts.Wall;
using UnityEngine;

namespace HumanHole.Scripts.LevelLogic
{
    public class LevelBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        private LevelStaticData _levelStaticData;
        private WallSpawner _wallSpawner;
        private LevelHandler _levelHandler;
        private AudioListenerHandler _audioListenerHandler;
        private AdHandler _adHandler;
        private LeaderBoardHandler _leaderBoardHandler;
        private LevelPointsHandler _levelPointsHandler;
        private LevelPauseHandler _levelPauseHandler;
        private ProfileDataHandler _profileDataHandler;
        private AnalyticsHandler _analyticsHandler;
        private LevelGoldHandler _levelGoldHandler;
        private GestureHandler _gestureHandler;
        private CollisionObserver _collisionObserver;
        private CameraLookAtPerson _cameraLookAtPerson;
        private ContoursHandler _contoursHandler;
        private TapHandHandler _tapTapHandler;
        private DirectionalLight _directionalLight;
        private LevelUI _levelUI;
        private Level _level;
        private CharactersSpawner _charactersSpawner;
        private Cameras _cameras;
        private SkyBox _skyBox;

        public async void Initial(GameStateMachine gameStateMachine, AllServices allServices, LevelsStaticData levelsStaticData)
        {
            Progress progress = allServices.Single<IPersistentProgressService>().Progress;
            ISaveLoadService saveLoadService = allServices.Single<ISaveLoadService>();
            IAdsService adsService = allServices.Single<IAdsService>();
            IAuthorizationService authorizationService = allServices.Single<IAuthorizationService>();
            IProfileDataService profileDataService = allServices.Single<IProfileDataService>();
            IAnalyticsService analyticsService = allServices.Single<IAnalyticsService>();
            ILeaderBoardService leaderBoardService = allServices.Single<ILeaderBoardService>();
            IDownloadService downloadService = allServices.Single<IDownloadService>();
            IFactoryService factoryService = allServices.Single<IFactoryService>();
            IRenderTextureService renderTextureService = allServices.Single<IRenderTextureService>();
            _levelStaticData = levelsStaticData.GetLevelStaticDataByNumber(progress.LevelsProgress.LevelNumber);
            await CreateEntities(factoryService);
            InitialEntities(gameStateMachine, progress, saveLoadService, leaderBoardService, downloadService, authorizationService, adsService, 
                profileDataService, analyticsService, factoryService, renderTextureService, levelsStaticData);
            enabled = true;
        }

        private async Task CreateEntities(IFactoryService factoryService)
        {
            Task<Level> levelTask = factoryService.CreateAsync<Level>(AssetsAddress.LevelPath);
            Task<DirectionalLight> directionalLightTask = factoryService.CreateAsync<DirectionalLight>(AssetsAddress.DirectionalLightPath);
            Task<Cameras> camerasTask = factoryService.CreateAsync<Cameras>(AssetsAddress.CamerasPath);
            Task<LevelUI> levelUITask = factoryService.CreateAsync<LevelUI>(AssetsAddress.LevelUIPath);
            Task<TapHandHandler> tapTapHandlerTask = factoryService.CreateAsync<TapHandHandler>(AssetsAddress.TapTapHandlerPath);
            Task<ContoursHandler> contoursHandlerTask = factoryService.CreateAsync<ContoursHandler>(AssetsAddress.ContoursHandlerPath);
            Task<CharactersSpawner> charactersSpawnerTask = factoryService.CreateAsync<CharactersSpawner>(AssetsAddress.CharactersSpawnerPath);
            await Task.WhenAll(levelTask, tapTapHandlerTask, contoursHandlerTask, directionalLightTask, levelUITask,camerasTask,  charactersSpawnerTask);

            _level = levelTask.Result;
            _tapTapHandler = tapTapHandlerTask.Result;
            _contoursHandler = contoursHandlerTask.Result;
            _directionalLight = directionalLightTask.Result;
            _levelUI = levelUITask.Result;
            _charactersSpawner = charactersSpawnerTask.Result;
            _cameras = camerasTask.Result;

            _audioListenerHandler = new AudioListenerHandler();
            _wallSpawner = new WallSpawner();
            _levelHandler = new LevelHandler();
            _adHandler = new AdHandler();
            _levelPauseHandler = new LevelPauseHandler();
            _collisionObserver = new CollisionObserver();
            _gestureHandler = new GestureHandler();
            _levelPointsHandler = new LevelPointsHandler();
            _leaderBoardHandler = new LeaderBoardHandler();
            _levelGoldHandler = new LevelGoldHandler();
            _profileDataHandler = new ProfileDataHandler();
            _analyticsHandler = new AnalyticsHandler();
            _cameraLookAtPerson = new CameraLookAtPerson();
            _skyBox = new SkyBox();
        }

        private void InitialEntities(GameStateMachine gameStateMachine, Progress progress,
            ISaveLoadService saveLoadService, ILeaderBoardService leaderBoardService,
            IDownloadService downloadService, IAuthorizationService authorizationService, IAdsService adsService,
            IProfileDataService profileDataService, IAnalyticsService analyticsService, IFactoryService factoryService,
            IRenderTextureService renderTextureService, LevelsStaticData levelsStaticData)
        {
            _wallSpawner.Initial(_levelPauseHandler, _levelHandler, _levelStaticData.LevelWallsStaticData, factoryService);
            _levelUI.Initial(progress, _tapTapHandler, _charactersSpawner.Person, _wallSpawner, saveLoadService,
                gameStateMachine, leaderBoardService, downloadService, authorizationService, factoryService, levelsStaticData);
            _levelHandler.Initial(gameStateMachine, progress, saveLoadService, _wallSpawner, _collisionObserver,
                _levelUI.LevelPanelsStateMachine, _level.WaterCollider, _charactersSpawner.CharacterSpawner, _levelUI, this);
            _adHandler.Initial(_levelHandler, _levelUI.LevelPanelsStateMachine, adsService);
            _levelPauseHandler.Initial(_adHandler, _levelUI.LevelPanelsStateMachine);
            _collisionObserver.Initial(_wallSpawner);
            _contoursHandler.Initial(_wallSpawner, _levelHandler);
            _gestureHandler.Initial(_levelHandler, _levelPauseHandler, _charactersSpawner.Limbs, _cameras.MainCamera);
            _levelPointsHandler.Initial(_levelHandler, progress, saveLoadService, _levelUI.LevelPanelsStateMachine, _levelStaticData);
            _leaderBoardHandler.Initial(_levelHandler, _levelPointsHandler, leaderBoardService, authorizationService);
            _levelGoldHandler.Initial(_levelUI, _levelHandler, progress, saveLoadService, _adHandler, _levelUI.LevelPanelsStateMachine, _levelStaticData);
            _profileDataHandler.Initial(profileDataService);
            _analyticsHandler.Initial(_levelHandler, _adHandler, analyticsService, progress);
            _cameraLookAtPerson.Initial(_levelHandler, _charactersSpawner.Person, _cameras.MainCamera);
            _charactersSpawner.Initial(progress.CharactersProgress, _collisionObserver, _levelUI.LevelPanelsStateMachine);
            _level.Initial(_charactersSpawner.Person, _levelStaticData, factoryService);
            _skyBox.Initial(_levelStaticData);
            _cameras.Initial(renderTextureService);
        }

        private void OnEnable()
        {
            _levelUI.OnEnabled();
            _charactersSpawner.OnEnabled();
            _analyticsHandler.OnEnabled();
            _adHandler.OnEnabled();
            _levelHandler.OnEnabled();
            _profileDataHandler.OnEnabled();
            _contoursHandler.OnEnabled();
            _leaderBoardHandler.OnEnabled();
            _wallSpawner.OnEnabled();
            _levelPointsHandler.OnEnabled();
            _levelGoldHandler.OnEnabled();
            _gestureHandler.OnEnabled();
            _levelPauseHandler.OnEnabled();
            _collisionObserver.OnEnabled();
            _cameraLookAtPerson.OnEnabled();
        }

        private void Start()
        {
            _directionalLight.OnStarted();
            _charactersSpawner.OnStarted();
            _contoursHandler.OnStarted();
            _levelGoldHandler.OnStarted();
            _gestureHandler.OnStarted();
            _levelHandler.OnStarted();
            _levelUI.OnStarted();
        }

        private void OnDisable()
        {
            _levelUI.OnDisabled();
            _charactersSpawner.OnDisabled();
            _analyticsHandler.OnDisabled();
            _adHandler.OnDisabled();
            _levelHandler.OnDisabled();
            _profileDataHandler.OnDisabled();
            _contoursHandler.OnDisabled();
            _leaderBoardHandler.OnDisabled();
            _wallSpawner.OnDisabled();
            _levelPointsHandler.OnDisabled();
            _levelGoldHandler.OnDisabled();
            _gestureHandler.OnDisabled();
            _levelPauseHandler.OnDisabled();
            _collisionObserver.OnDisabled();
        }

        private void Update()
        {
            _audioListenerHandler.OnUpdated();
            _cameraLookAtPerson.OnUpdated();
        }
    }
}