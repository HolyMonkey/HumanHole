using System;
using System.Collections.Generic;
using UnityEngine;

public class WallSpawner : MonoBehaviour
{
    private int _index = 0;
    private LevelPauseHandler _levelPauseHandler;
    private LevelHandler _levelHandler;

    [SerializeField] private float _wallSpeed = 0;
    [SerializeField] private List<Wall> _walls;
    [SerializeField] private Color _color;

    private Wall _currentWall;
    private bool _spawnAllowed;

    public event Action<Wall> LeftPlayerZone;
    public event Action<Wall> Spawned;
    public event Action<Wall> Destroyed;
    public event Action AllWallsDestroyed;

    public void Initial(LevelPauseHandler levelPauseHandler, LevelHandler levelHandler)
    {
        _levelPauseHandler = levelPauseHandler;
        _levelHandler = levelHandler;
    }

    private void OnEnable()
    {
        _levelHandler.LevelLost += OnLevelLost;
        _levelPauseHandler.Pause += LevelPause;
        _levelPauseHandler.UnPause += LevelUnPause;
    }

    private void OnDisable()
    {
        _levelHandler.LevelLost -= OnLevelLost;
        _levelPauseHandler.Pause -= LevelPause;
        _levelPauseHandler.UnPause -= LevelUnPause;
    }

    public void Enable()
    {
        gameObject.SetActive(true);
    }

    public void StartSpawn()
    {
        AllowSpawn();
        Spawn();
    }

    private void AllowSpawn()
    {
        _spawnAllowed = true;
    }

    private void ForbidSpawn()
    {
        _spawnAllowed = false;
    }
    
    private void Spawn()
    {
        var wall = _walls[_index];
        _currentWall = Instantiate(wall, wall.transform.position, Quaternion.identity);
        _currentWall.Initialize(_wallSpeed, _color);
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
        ForbidSpawn();
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
        _spawnAllowed && _index < _walls.Count;

    private void LevelPause()
    {
        _currentWall?.StopMovement();
    }

    private void LevelUnPause()
    {
        _currentWall?.AllowMovement();
    }
}