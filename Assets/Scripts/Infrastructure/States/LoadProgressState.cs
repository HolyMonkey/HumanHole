public class LoadProgressState : IState
{
    private readonly GameStateMachine _gameStateMachine;
    private readonly IPersistentProgressService _progressService;
    private readonly ISaveLoadService _saveLoadService;

    public LoadProgressState(GameStateMachine gameStateMachine, IPersistentProgressService progressService,
        ISaveLoadService saveLoadService)
    {
        _gameStateMachine = gameStateMachine;
        _progressService = progressService;
        _saveLoadService = saveLoadService;
    }

    public void Enter()
    {
        LoadProgressOrCreateNew();
        _gameStateMachine.Enter<LoadLevelState, string, bool>(_progressService.Progress.LevelName(), false);
    }

    public void Exit()
    {
    }

    private void LoadProgressOrCreateNew()
    {
        _progressService.Progress = _saveLoadService.LoadProgress() ?? CreateProgress();
    }

    private static Progress CreateProgress() => 
        new Progress();
}