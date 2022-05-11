using System;
using System.Collections.Generic;
using HumanHole.Scripts.Handlers;
using HumanHole.Scripts.Infrastructure.Services.Factory;
using UnityEngine;

namespace HumanHole.Scripts.Wall
{
    public class WallSpawner
    {
        public event Action<Wall> LeftPlayerZone;
        public event Action<Wall> Spawned;
        public event Action<Wall> Destroyed;
        public event Action AllWallsDestroyed;
        
        private float _wallSpeed;
        private IReadOnlyList<Wall> _walls;
        private Color _color;
        private int _index;
        private LevelPauseHandler _levelPauseHandler;
        private LevelHandler _levelHandler;
        private Wall _currentWall;
        private bool _spawnAllowed;
        private IFactoryService _factoryService;
        
        public void Initial(LevelPauseHandler levelPauseHandler, LevelHandler levelHandler, LevelWallsStaticData levelWallsStaticData, IFactoryService factoryService)
        {
            _factoryService = factoryService;
            _levelPauseHandler = levelPauseHandler;
            _levelHandler = levelHandler;
            _wallSpeed = levelWallsStaticData.WallSpeed;
            _walls = levelWallsStaticData.Walls;
            _color = levelWallsStaticData.Color;
        }

        public void OnEnabled()
        {
            _levelHandler.LevelLost += OnLevelLost;
            _levelPauseHandler.Pause += LevelPause;
            _levelPauseHandler.UnPause += LevelUnPause;
        }

        public void OnDisabled()
        {
            _levelHandler.LevelLost -= OnLevelLost;
            _levelPauseHandler.Pause -= LevelPause;
            _levelPauseHandler.UnPause -= LevelUnPause;
        }

        public void StartSpawn()
        {
            AllowSpawn();
            Spawn();
        }

        private void AllowSpawn() => 
            _spawnAllowed = true;

        private void ForbidSpawn() => 
            _spawnAllowed = false;

        private void Spawn()
        {
            Wall wall = _walls[_index];
            _currentWall = _factoryService.Create(wall, wall.transform.position, Quaternion.identity);
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
    
        private void OnLevelLost() => 
            ForbidSpawn();

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

        private void LevelPause() => 
            _currentWall?.StopMovement();

        private void LevelUnPause() => 
            _currentWall?.AllowMovement();
    }
}