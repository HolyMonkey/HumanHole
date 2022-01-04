using UnityEngine;

    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        [SerializeField] private LoadingCurtain _сurtainPrefab;

        public static GameBootstrapper Instance { get; private set; }
        public Game Game { get; private set; }


        private void Awake()
        {
            Game = new Game(this, Instantiate(_сurtainPrefab));
            Game.StateMachine.Enter<BootstrapState>();

            DontDestroyOnLoad(this);
        }
    }