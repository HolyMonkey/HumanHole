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
    [SerializeField] private AdHandler _adHandler;
    [SerializeField] private LeaderBoardHandler _leaderBoardHandler;
    [SerializeField] private LevelPointsHandler _levelPointsHandler;
    [SerializeField] private WallSpawner _wallSpawner;
    [SerializeField] private WaterCollider _waterCollider;
    [SerializeField] private CollisionObserver _collisionObserver;
    [SerializeField] private LevelPauseHandler _levelPauseHandler;
    [SerializeField] private ProfileDataHandler _profileDataHandler;
    [SerializeField] private AnalyticsHandler _analyticsHandler;
    [SerializeField] private TapHandHandler _tapHandHandler;
    [SerializeField] private Person _person;
    [SerializeField] private LookAtPerson _lookAtPerson;

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
        PlayerProfileDataPanel profileDataPanel = _levelUI.PlayerProfileDataPanel;
        SettingsPanel settingsPanel = _levelUI.SettingsPanel;
        LeaderBoardPanel leaderBoardPanel = _levelUI.LeaderBoardPanel;
        LevelPanelsStateMachine levelPanelsStateMachine = _levelUI.LevelPanelsStateMachine;
        
        _levelHandler.Initial(gameStateMachine, progress, saveLoadService, _wallSpawner, _collisionObserver, levelPanelsStateMachine, _waterCollider);
        _levelUI.Initial(_levelHandler, progress, _tapHandHandler, _person, _wallSpawner, saveLoadService, gameStateMachine);
        _contoursHandler.Initial(_wallSpawner, _levelHandler);
        _adHandler.Initial(_levelHandler, _levelUI, adsService, rewardService);
        _gestureHandler.Initial(_levelHandler, _levelPauseHandler);
        _levelPointsHandler.Initial(_wallSpawner, _levelUI, _levelHandler, progress, saveLoadService, _adHandler);
        _leaderBoardHandler.Initial(_levelHandler, _levelPointsHandler, leaderBoardService, authorizationService);
        _levelPauseHandler.Initial(_adHandler, settingsPanel, profileDataPanel, leaderBoardPanel);
        _wallSpawner.Initial(_levelPauseHandler, _levelHandler);
        _profileDataHandler.Initial(profileDataService, profileDataPanel);
        _analyticsHandler.Initial(_levelHandler, _adHandler, analyticsService, progress);
        _collisionObserver.Initial(_wallSpawner);
        _lookAtPerson.Initial(_levelHandler, _person);
        _person.Initial(_collisionObserver);
    }

    private void OnEnable()
    {
        _person.Enable();
        _analyticsHandler.Enable();
        _adHandler.Enable();
        _contoursHandler.Enable();
        _wallSpawner.Enable();
        _levelHandler.Enable();
        _levelPauseHandler.Enable();
        _levelPointsHandler.Enable();
        _leaderBoardHandler.Enable();
        _gestureHandler.Enable();
        _profileDataHandler.Enable();
        _lookAtPerson.Enable();
        _collisionObserver.Enable();
    }
}