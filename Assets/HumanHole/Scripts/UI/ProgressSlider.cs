using UnityEngine;
using UnityEngine.UI;

public class ProgressSlider : MonoBehaviour
{
    private Transform _currentWallTransform;
    private Slider _slider;
    private float _startDistance;
    private bool _isAllowed;
    private WallSpawner _wallSpawner;
    private Transform _personTransform;
    
    public void Initial(WallSpawner wallSpawner, Person person)
    {
        _slider = GetComponent<Slider>();
        _personTransform = person.transform;
        _wallSpawner = wallSpawner;
    }

    public void Enable()
    {
        gameObject.SetActive(true);
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
    
    private void AllowCalculateDistance() => 
        _isAllowed = true;

    private void ForbidCalculateDistance() => 
        _isAllowed = false;

    private void OnWallLeftPlayerZone(Wall obj) => 
        ForbidCalculateDistance();

    private void OnWallSpawned(Wall wall)
    {
        SetSliderValue(0);
        _currentWallTransform = wall.transform;
        _startDistance = GetCurrentDistance();
        AllowCalculateDistance();
    }

    private void OnWallDestroyed(Wall wall) => 
        _currentWallTransform = null;

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

    private void SetSliderValue(float value) => 
        _slider.value = value;

    private float GetCurrentDistance() => 
        Vector3.Distance(_currentWallTransform.position, _personTransform.position);
}
