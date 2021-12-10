using System;
using UnityEngine;

public class СollisionObserver : MonoBehaviour
{
    private Transform _currentWallTransform;
    private bool _touchedPerson;
    
    [SerializeField] private WallSpawner _wallSpawner;
    [SerializeField] private Transform _personTransorm;
    
    public event Action WallHitPerson;
    
    private void OnEnable()
    {
        _wallSpawner.Spawned += WallSpawned;
        _wallSpawner.Destroyed += WallDestroyed;
    }

    private void OnDisable()
    {
        _wallSpawner.Spawned -= WallSpawned;
        _wallSpawner.Destroyed -= WallDestroyed;
    }

    private void WallSpawned()
    {
        _touchedPerson = false;
        Wall wall = _wallSpawner.СurrentWall;
        _currentWallTransform = wall.transform;
        wall.TouchedPerson += OnWallHitPerson;
    }

    private void WallDestroyed()
    {
        Wall wall = _wallSpawner.СurrentWall;
        wall.TouchedPerson -= OnWallHitPerson;
    }

    private void OnWallHitPerson()
    {
        _touchedPerson = true;
        WallHitPerson?.Invoke();
    }

    private void Update()
    {
        if (_currentWallTransform != null)
        {
            if (_currentWallTransform.position.z < _personTransorm.position.z)
            {
                if (!_touchedPerson)
                {
                    
                }
            }
        }
    }
}
