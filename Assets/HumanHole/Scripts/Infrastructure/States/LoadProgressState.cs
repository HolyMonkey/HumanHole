using HumanHole.Scripts.Data;
using HumanHole.Scripts.Infrastructure.Services.PersistentProgress;
using HumanHole.Scripts.Infrastructure.Services.SaveLoad;

namespace HumanHole.Scripts.Infrastructure.States
{
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

        public void Enter() => 
            LoadProgressOrCreateNew();

        public void Exit()
        {
        }

        private void LoadProgressOrCreateNew()
        {
            _saveLoadService.Loaded += OnProgressLoaded;
            _saveLoadService.LoadProgress();
        }

        private void OnProgressLoaded(Progress progress)
        {
            _saveLoadService.Loaded -= OnProgressLoaded;
            _progressService.Progress = progress ?? CreateProgress();
            _gameStateMachine.Enter<LoadLevelState, string, bool>(_progressService.Progress.LevelsProgress.LevelName(), false);
        }

        private static Progress CreateProgress() => 
            new Progress();
    }
}