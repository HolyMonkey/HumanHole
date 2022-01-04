using CodeBase.Infrastructure;
using CodeBase.Infrastructure.Services.Ads;

public class BootstrapState : IState
{
    private const string Initial = "Initial";
    private readonly GameStateMachine _stateMachine;
    private readonly SceneLoader _sceneLoader;
    private readonly AllServices _services;

    public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, AllServices services)
    {
        _stateMachine = stateMachine;
        _sceneLoader = sceneLoader;
        _services = services;

        RegisterServices();
    }

    public void Enter()
    {
        _services.Single<ICursorService>().Disable();
        _sceneLoader.Load(Initial, onLoaded: EnterLoadLevel);
    }

    public void Exit()
    {
    }

    private void EnterLoadLevel() =>
        _stateMachine.Enter<LoadProgressState>();

    private void RegisterServices()
    {
        _services.RegisterSingle<IGameStateMachine>(_stateMachine);
        _services.RegisterSingle<IAdsService>(new AdsService());
        _services.RegisterSingle<IPersistentProgressService>(new PersistentProgressService());
        _services.Single<IPersistentProgressService>();
        _services.RegisterSingle<ISaveLoadService>(new SaveLoadService(
            _services.Single<IPersistentProgressService>()));
        _services.RegisterSingle<ICursorService>(new CursorService());
    }
}