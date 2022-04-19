using HumanHole.Scripts.Logic;

namespace HumanHole.Scripts.Infrastructure.States
{
    public class LoadLevelState : IPayloadedState<string, bool>
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _curtain;
        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingCurtain curtain)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _curtain = curtain;
        }

        public void Enter(string sceneName, bool restartAllowed = false)
        {
            _curtain.Show();
            _sceneLoader.Load(sceneName, OnLoaded, restartAllowed);
        }

        public void Exit() =>
            _curtain.Hide();

        private void OnLoaded()
        {
            _stateMachine.Enter<GameLoopState>();
        }

    }
}