using System;
using UnityEngine;

public class CollisionObserver : MonoBehaviour
{
    private Transform _currentWallTransform;
    private bool _touchedPerson;
    
    [SerializeField] private WallSpawner _wallSpawner;
    [SerializeField] private Transform _personTransorm;
    
    public event Action WallCollidedPlayer;
    
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

    private void WallSpawned(Wall wall)
    {
        _touchedPerson = false;
        _currentWallTransform = wall.transform;
        wall.TouchedPlayer += OnWallHitPlayer;
    }

    private void WallDestroyed(Wall wall)
    {
        wall.TouchedPlayer -= OnWallHitPlayer;
    }

    private void OnWallHitPlayer()
    {
        _touchedPerson = true;
        WallCollidedPlayer?.Invoke();
    }
}
