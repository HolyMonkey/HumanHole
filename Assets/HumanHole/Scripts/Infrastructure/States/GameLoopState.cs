using HumanHole.Scripts.Infrastructure.Services;
using HumanHole.Scripts.Infrastructure.Services.Factory;
using HumanHole.Scripts.LevelLogic;

namespace HumanHole.Scripts.Infrastructure.States
{
    public class GameLoopState : IState
    {
        private IFactoryService _factoryService;
        private LevelBootstrapper _levelBootstrapper;
        private GameStateMachine _gameStateMachine;
        private AllServices _allServices;
        private LevelsStaticData _levelsStaticData;

        public GameLoopState(GameStateMachine gameStateMachine, AllServices allServices, LevelBootstrapper levelBootstrapper, LevelsStaticData levelsStaticData)
        {
            _levelsStaticData = levelsStaticData;
            _allServices = allServices;
            _gameStateMachine = gameStateMachine;
            _levelBootstrapper = levelBootstrapper;
            _factoryService = allServices.Single<IFactoryService>();
        }

        public void Exit()
        {
        }

        public void Enter()
        {
            LevelBootstrapper levelBootstrapperObject = _factoryService.Create(_levelBootstrapper);
            levelBootstrapperObject.Initial(_gameStateMachine, _allServices, _levelsStaticData);
        }
    }
}