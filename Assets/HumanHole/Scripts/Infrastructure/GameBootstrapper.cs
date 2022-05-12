using HumanHole.Scripts.Infrastructure.States;
using HumanHole.Scripts.LevelLogic;
using HumanHole.Scripts.Logic;
using UnityEngine;

namespace HumanHole.Scripts.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        [SerializeField] private LoadingCurtain _сurtainPrefab;
        [SerializeField] private LevelBootstrapper _levelBootstrapper;
        [SerializeField] private LevelsStaticData _levelsStaticData;
        
        public Game Game { get; private set; }
        
        private void Awake()
        {
            Game = new Game(this, Instantiate(_сurtainPrefab), _levelBootstrapper, _levelsStaticData);
            Game.StateMachine.Enter<BootstrapState>();

            DontDestroyOnLoad(this);
        }
    }
}