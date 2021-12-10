using UnityEngine;
using UnityEngine.UI;

public class ProgressSlider : MonoBehaviour
{
    private Transform _currentWallTransform;
    private Slider _slider;
    
    [SerializeField] private WallSpawner _wallSpawner;
    [SerializeField] private Transform _personTransform;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    private void OnEnable()
    {
        _wallSpawner.Spawned += OnWallSpawned;
        _wallSpawner.Destroyed += OnWallDestroyed;
    }

    private void OnDisable()
    {
        _wallSpawner.Spawned -= OnWallSpawned;
        _wallSpawner.Destroyed -= OnWallDestroyed;
    }

    private void OnWallDestroyed()
    {
        _currentWallTransform = null;
    }

    private void OnWallSpawned()
    {
        _currentWallTransform = _wallSpawner.СurrentWall.transform;
    }

    private void Update()
    {
        if (_currentWallTransform != null)
        {
            _slider.value = _currentWallTransform.transform.position.z / _personTransform.position.z;
        }
            
    }
}
