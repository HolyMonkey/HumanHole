using HumanHole.Scripts.Handlers;

namespace HumanHole.Scripts.Gesture
{
    public class GestureHandler
    {
        private LevelHandler _levelHandler;
        private LevelPauseHandler _levelPauseHandler;
        private Limbs _limbs;

        public void Initial(LevelHandler levelHandler, LevelPauseHandler levelPauseHandler, Limbs limbs, UnityEngine.Camera mainCamera)
        {
            _levelPauseHandler = levelPauseHandler;
            _levelHandler = levelHandler;
            _limbs = limbs;
            
            foreach (Limb limb in _limbs.LimbsCollection)
                limb.Initial(mainCamera);
        }

        public void OnEnabled()
        {
            _levelHandler.LevelStarted += OnLevelStarted;
            _levelHandler.LevelWon += OnLevelWon;
            _levelHandler.LevelLost += OnLevelLost;
            _levelPauseHandler.Pause += OnLevelPause;
            _levelPauseHandler.UnPause += OnLevelUnPause;
        }

        public void OnDisabled()
        {
            _levelHandler.LevelStarted -= OnLevelStarted;
            _levelHandler.LevelWon -= OnLevelWon;
            _levelHandler.LevelLost -= OnLevelLost;
            _levelPauseHandler.Pause -= OnLevelPause;
            _levelPauseHandler.UnPause -= OnLevelUnPause;
        }

        public void OnStarted() => 
            AllowMovement();

        private void OnLevelWon() => 
            DeactivateLimbs();

        private void OnLevelLost() => 
            DeactivateLimbs();

        private void OnLevelStarted() => 
            ActivateLimbs();

        private void ActivateLimbs()
        {
            foreach (Limb limb in _limbs.LimbsCollection)
                limb.Enable();
        }

        private void DeactivateLimbs()
        {
            foreach (Limb limb in _limbs.LimbsCollection)
                limb.Disable();
        }

        private void OnLevelPause() => 
            ForbidMovement();

        private void OnLevelUnPause() => 
            AllowMovement();

        private void AllowMovement()
        {
            foreach (Limb limb in _limbs.LimbsCollection)
                limb.AllowMovement();
        }

        private void ForbidMovement()
        {
            foreach (Limb limb in _limbs.LimbsCollection)
                limb.ForbidMovement();
        }
    }
}