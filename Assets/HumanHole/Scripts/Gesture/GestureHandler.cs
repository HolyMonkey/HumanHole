using HumanHole.Scripts.Handlers;
using UnityEngine;

namespace HumanHole.Scripts.Gesture
{
    public class GestureHandler : MonoBehaviour
    {
        private LevelHandler _levelHandler;
        private LevelPauseHandler _levelPauseHandler;
        
        [SerializeField] private Limbs _limbsController;

        public void Initial(LevelHandler levelHandler, LevelPauseHandler levelPauseHandler)
        {
            _levelPauseHandler = levelPauseHandler;
            _levelHandler = levelHandler;
        }

        public void Enable()
        {
            gameObject.SetActive(true);
            AllowMovement();
        }

        private void OnEnable()
        {
            _levelHandler.LevelStarted += OnLevelStarted;
            _levelHandler.LevelWon += OnLevelWon;
            _levelHandler.LevelLost += OnLevelLost;
            _levelPauseHandler.Pause += OnLevelPause;
            _levelPauseHandler.UnPause += OnLevelUnPause;
        }

        private void OnDisable()
        {
            _levelHandler.LevelStarted -= OnLevelStarted;
            _levelHandler.LevelWon -= OnLevelWon;
            _levelHandler.LevelLost -= OnLevelLost;
            _levelPauseHandler.Pause -= OnLevelPause;
            _levelPauseHandler.UnPause -= OnLevelUnPause;
        }

        private void OnLevelWon() => 
            DeactivateLimbs();

        private void OnLevelLost() => 
            DeactivateLimbs();

        private void OnLevelStarted() => 
            ActivateLimbs();

        private void ActivateLimbs()
        {
            foreach (Limb limb in _limbsController.LimbsCollection)
                limb.Enable();
        }

        private void DeactivateLimbs()
        {
            foreach (Limb limb in _limbsController.LimbsCollection)
                limb.Disable();
        }

        private void OnLevelPause() => 
            ForbidMovement();

        private void OnLevelUnPause() => 
            AllowMovement();

        private void AllowMovement()
        {
            foreach (Limb limb in _limbsController.LimbsCollection)
                limb.AllowMovement();
        }

        private void ForbidMovement()
        {
            foreach (Limb limb in _limbsController.LimbsCollection)
                limb.ForbidMovement();
        }
    }
}