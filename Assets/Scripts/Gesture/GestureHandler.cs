using UnityEngine;

public class GestureHandler : MonoBehaviour
{
    [SerializeField] private Limb[] _limbs;
    [SerializeField] private LevelHandler _levelHandler;

    public void Enable()
    {
        gameObject.SetActive(true);
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        _levelHandler.LevelStarted += OnLevelStarted;
        _levelHandler.LevelWon += OnLevelWon;
        _levelHandler.LevelLost += OnLevelLost;
    }

    private void OnDisable()
    {
        _levelHandler.LevelStarted -= OnLevelStarted;
        _levelHandler.LevelWon -= OnLevelWon;
        _levelHandler.LevelLost -= OnLevelLost;
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
        foreach (var item in _limbs)
            item.Enable();
    }
    
    private void DeactivateLimbs()
    {
        foreach (var item in _limbs)
            item.Disable();
    }
}
