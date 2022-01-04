using UnityEngine;
using UnityEngine.UI;

public class ProgressSlider : MonoBehaviour
{
    private Transform _currentWallTransform;
    private Slider _slider;
    private float _startDistance;
    private bool _isAllowed;
    
    [SerializeField] private WallSpawner _wallSpawner;
    [SerializeField] private Transform _personTransform;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    private void OnEnable()
    {
        _wallSpawner.Spawned += OnWallSpawned;
        _wallSpawner.LeftPlayerZone += OnWallLeftPlayerZone;
        _wallSpawner.Destroyed += OnWallDestroyed;
    }

    private void OnDisable()
    {
        _wallSpawner.Spawned -= OnWallSpawned;
        _wallSpawner.Destroyed -= OnWallDestroyed;
        _wallSpawner.LeftPlayerZone -= OnWallLeftPlayerZone;
    }
    
    private void AllowCalculateDistance()
    {
        _isAllowed = true;
    }

    private void ForbidCalculateDistance()
    {
        _isAllowed = false;
    }

    private void OnWallLeftPlayerZone(Wall obj)
    {
        ForbidCalculateDistance();
    }

    private void OnWallSpawned(Wall wall)
    {
        SetSliderValue(0);
        _currentWallTransform = wall.transform;
        _startDistance = GetCurrentDistance();
        AllowCalculateDistance();
    }

    private void OnWallDestroyed(Wall wall)
    {
        _currentWallTransform = null;
    }

    private void Update()
    {
        if (CanChangeDistance())
        {
            CalculateDistance();
        }
            
    }

    private bool CanChangeDistance() => 
        _currentWallTransform != null && _isAllowed;

    private void CalculateDistance()
    {
        var distance = GetCurrentDistance();
        var value = 1 - distance / _startDistance;
        SetSliderValue(value);
    }

    private void SetSliderValue(float value)
    {
        _slider.value = value;
    }

    private float GetCurrentDistance() => 
        Vector3.Distance(_currentWallTransform.position, _personTransform.position);
}
