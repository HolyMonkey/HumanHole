using UnityEngine;

public class GestureHandler : MonoBehaviour
{
    private LevelHandler _levelHandler;
    private LevelPauseHandler _levelPauseHandler;

    [SerializeField] private Limb[] _limbs;

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
        foreach (var limb in _limbs)
            limb.Enable();
    }

    private void DeactivateLimbs()
    {
        foreach (var limb in _limbs)
            limb.Disable();
    }

    private void OnLevelPause() => 
        ForbidMovement();

    private void OnLevelUnPause() => 
        AllowMovement();

    private void AllowMovement()
    {
        foreach (var limb in _limbs)
            limb.AllowMovement();
    }

    private void ForbidMovement()
    {
        foreach (var limb in _limbs)
            limb.ForbidMovement();
    }
}