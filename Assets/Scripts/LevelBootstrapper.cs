using CodeBase.Infrastructure.Services.LeaderBoard;
using CodeBase.Infrastructure.Services.Profile;
using Handlers;
using Infrastructure.Services.Analytics;
using UnityEngine;

public class LevelBootstrapper : MonoBehaviour
{
    [SerializeField] private LevelHandler _levelHandler;
    [SerializeField] private GestureHandler _gestureHandler;
    [SerializeField] private ContoursHandler _contoursHandler;
    [SerializeField] private LevelUI _levelUI;
    [SerializeField] private LevelPanelsStateMachine _levelPanelsStateMachine;
    [SerializeField] private AdHandler _adHandler;
    [SerializeField] private AuthorizationHandler _authorizationHandler;
    [SerializeField] private LeaderBoardHandler _leaderBoardHandler;
    [SerializeField] private LevelPointsHandler _levelPointsHandler;
    [SerializeField] private WallSpawner _wallSpawner;
    [SerializeField] private WaterCollider _waterCollider;
    [SerializeField] private CollisionObserver _collisionObserver;
    [SerializeField] private LevelPauseHandler _levelPauseHandler;
    [SerializeField] private ProfileDataHandler _profileDataHandler;
    [SerializeField] private AnalyticsHandler _analyticsHandler;

    public void Awake()
    {
        Initial();
    }
    
    private void Initial()
    {
        Game game = Game.Instance;
        GameStateMachine gameStateMachine = game.StateMachine;
        AllServices allServices = game.AllServices;
        Progress progress = allServices.Single<IPersistentProgressService>().Progress;
        ISaveLoadService saveLoadService = allServices.Single<ISaveLoadService>();
        IAdsService adsService = allServices.Single<IAdsService>();
        IRewardService rewardService = allServices.Single<IRewardService>();
        IAuthorizationService authorizationService = allServices.Single<IAuthorizationService>();
        IProfileDataService profileDataService = allServices.Single<IProfileDataService>();
        IAnalyticsService analyticsService = allServices.Single<IAnalyticsService>();
        ILeaderBoardService leaderBoardService = allServices.Single<ILeaderBoardService>();
        
        _levelPanelsStateMachine.Initial();
        _levelHandler.Initial(gameStateMachine, progress, saveLoadService, _wallSpawner, _collisionObserver, _levelPanelsStateMachine, _waterCollider);
        _levelUI.Initial(_levelHandler, progress);
        _contoursHandler.Initial(_wallSpawner, _levelHandler);
        _adHandler.Initial(_levelHandler, _levelUI, adsService, rewardService);
        _gestureHandler.Initial(_levelHandler, _levelPauseHandler);
        _authorizationHandler.Initial(authorizationService);
        _levelPointsHandler.Initial(_wallSpawner, _levelUI, _levelHandler, progress, saveLoadService, _adHandler);
        _leaderBoardHandler.Initial(_levelHandler, _levelPointsHandler, leaderBoardService);
        _levelPauseHandler.Initial(_adHandler);
        _wallSpawner.Initial(_levelPauseHandler, _levelHandler);
        _profileDataHandler.Initial(profileDataService);
        _analyticsHandler.Initial(_levelHandler, _adHandler, analyticsService, progress);
    }

    private void OnEnable()
    {
        _analyticsHandler.Enable();
        _adHandler.Enable();
        _contoursHandler.Enable();
        _wallSpawner.Enable();
        _levelHandler.Enable();
        _authorizationHandler.Enable();
        _levelPauseHandler.Enable();
        _levelPointsHandler.Enable();
        _leaderBoardHandler.Enable();
        _gestureHandler.Enable();
        _profileDataHandler.Enable();
    }
}