using HumanHole.Scripts.Data;
using HumanHole.Scripts.Infrastructure.Services;
using HumanHole.Scripts.Infrastructure.Services.PersistentProgress;
using HumanHole.Scripts.Infrastructure.Services.SaveLoad;
using HumanHole.Scripts.LevelLogic;

namespace HumanHole.Scripts.Infrastructure.States
{
    public class LoadProgressState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IPersistentProgressService _progressService;
        private readonly ISaveLoadService _saveLoadService;
        private LevelsStaticData _levelsStaticData;

        public LoadProgressState(GameStateMachine gameStateMachine, AllServices allServices,
            LevelsStaticData levelsStaticData)
        {
            _levelsStaticData = levelsStaticData;
            _gameStateMachine = gameStateMachine;
            _progressService = allServices.Single<IPersistentProgressService>();
            _saveLoadService = allServices.Single<ISaveLoadService>();
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
            _progressService.Progress.LevelsProgress.SetMaxLevelNumber(_levelsStaticData.LevelsStaticDataList.Count);
            _gameStateMachine.Enter<LoadLevelState, string, bool>(_progressService.Progress.LevelsProgress.LevelName, false);
        }

        private static Progress CreateProgress() => 
            new Progress();
    }
}