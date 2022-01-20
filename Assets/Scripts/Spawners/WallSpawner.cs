using System;
using System.Collections.Generic;
using UnityEngine;

public class WallSpawner : MonoBehaviour
{
    private int _index = 0;
    
    [SerializeField] private float _wallSpeed = 0;
    [SerializeField] private List<Wall> _walls;
    [SerializeField] private LevelHandler _levelHandler;
    [SerializeField] private LevelPauseHandler _levelPauseHandler;
    
    private Wall _currentWall;

    public event Action<Wall> LeftPlayerZone;
    public event Action<Wall> Spawned;
    public event Action<Wall> Destroyed;
    public event Action AllWallsDestroyed;
    
    private void OnEnable()
    {
        _levelHandler.LevelLost += OnLevelLost;
        _levelPauseHandler.OnPause += LevelOnPause;
        _levelPauseHandler.OffPause += LevelOffPause;
    }

    private void OnDisable()
    {
        _levelHandler.LevelLost -= OnLevelLost;
        _levelPauseHandler.OnPause -= LevelOnPause;
        _levelPauseHandler.OffPause -= LevelOffPause;
    }

    public void StartSpawn()
    {
        Spawn();
    }

    private void Spawn()
    {
        var wall = _walls[_index];
        _currentWall = Instantiate(wall, wall.transform.position, Quaternion.identity);
        _currentWall.Initialize(_wallSpeed);
        _currentWall.LeftPlayerZone += OnLeftPlayerZoneZone;
        Spawned?.Invoke(_currentWall);
        _currentWall.Destroyed += Destroy;
        _index++;
    }
    

    private void OnLeftPlayerZoneZone()
    {
        _currentWall.LeftPlayerZone -= OnLeftPlayerZoneZone;
        LeftPlayerZone?.Invoke(_currentWall);
    }
    
    private void OnLevelLost()
    {
        _currentWall?.StopMovement();
    }

    private void Destroy()
    {
        Destroyed?.Invoke(_currentWall);
        if (CanSpawn())
            Spawn();
        else
            AllWallsDestroyed?.Invoke();
    }

    private bool CanSpawn() => 
        _index < _walls.Count;

    private void LevelOnPause()
    {
        _currentWall?.StopMovement();
    }

    private void LevelOffPause()
    {
        _currentWall?.AllowMovement();
    }
}