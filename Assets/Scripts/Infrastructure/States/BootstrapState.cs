using CodeBase.Infrastructure;
using CodeBase.Infrastructure.Services.Ads;
using CodeBase.Infrastructure.Services.LeaderBoard;
using CodeBase.Infrastructure.Services.Profile;
using Infrastructure.Services.Analytics;

public class BootstrapState : IState
{
    private readonly GameStateMachine _stateMachine;
    private readonly AllServices _services;
    private readonly ICoroutineRunner _coroutineRunner;

    public BootstrapState(GameStateMachine stateMachine, ICoroutineRunner coroutineRunner,
        AllServices services)
    {
        _coroutineRunner = coroutineRunner;
        _stateMachine = stateMachine;
        _services = services;

        RegisterServices();
    }

    public void Enter()
    {
        _stateMachine.Enter<LoadProgressState>();
    }

    public void Exit()
    {
    }

    private void RegisterServices()
    {
        _services.RegisterSingle<IGameStateMachine>(_stateMachine);
        _services.RegisterSingle<IAdsService>(new AdsService());
        _services.RegisterSingle<IPersistentProgressService>(new PersistentProgressService());
        _services.RegisterSingle<ISaveLoadService>(new SaveLoadService(_services.Single<IPersistentProgressService>()));
        _services.RegisterSingle<IAuthorizationService>(new AuthorizationService(_coroutineRunner));
        _services.RegisterSingle<IProfileDataService>(new ProfileDataService(_services.Single<IAuthorizationService>()));
        _services.RegisterSingle<ILeaderBoardService>(new LeaderBoardService());
        _services.RegisterSingle<IRewardService>(new RewardService(
            _services.Single<IPersistentProgressService>(),
            _services.Single<ISaveLoadService>()));
        _services.RegisterSingle<IRenderTextureService>(new RenderTextureService());
        _services.RegisterSingle<IAnalyticsService>(new AnalyticsService());
    }
}