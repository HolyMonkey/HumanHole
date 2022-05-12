using HumanHole.Scripts.Infrastructure.Services;
using HumanHole.Scripts.Infrastructure.States;
using HumanHole.Scripts.LevelLogic;
using HumanHole.Scripts.Logic;

namespace HumanHole.Scripts.Infrastructure
{
    public class Game
    {
        public static Game Instance { get; private set; }
        public GameStateMachine StateMachine { get; private set; }
        public AllServices AllServices { get; private set; }
        public Game(ICoroutineRunner coroutineRunner, LoadingCurtain curtain, LevelBootstrapper levelBootstrapper, LevelsStaticData levelsStaticData)
        {
            Instance ??= this;
            AllServices = new AllServices();
            StateMachine = new GameStateMachine(coroutineRunner, curtain, AllServices, levelBootstrapper, levelsStaticData);
        }
    }
}