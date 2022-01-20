using UnityEngine;

public class GestureHandler : MonoBehaviour
{
    [SerializeField] private Limb[] _limbs;
    [SerializeField] private LevelHandler _levelHandler;
    [SerializeField] private LevelPauseHandler _levelPauseHandler;

    public void Initial()
    {
        AllowMovement();
    }

    private void OnEnable()
    {
        _levelHandler.LevelStarted += OnLevelStarted;
        _levelHandler.LevelWon += OnLevelWon;
        _levelHandler.LevelLost += OnLevelLost;
        _levelPauseHandler.OnPause += LevelOnPause;
        _levelPauseHandler.OffPause += LevelOffPause;
    }

    private void OnDisable()
    {
        _levelHandler.LevelStarted -= OnLevelStarted;
        _levelHandler.LevelWon -= OnLevelWon;
        _levelHandler.LevelLost -= OnLevelLost;
        _levelPauseHandler.OnPause -= LevelOnPause;
        _levelPauseHandler.OffPause -= LevelOffPause;
    }

    private void OnLevelWon()
    {
        DeactivateLimbs();
    }

    private void OnLevelLost()
    {
        DeactivateLimbs();
    }

    private void OnLevelStarted()
    {
        ActivateLimbs();
    }

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

    private void LevelOnPause()
    {
        ForbidMovement();
    }

    private void LevelOffPause()
    {
        AllowMovement();
    }

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