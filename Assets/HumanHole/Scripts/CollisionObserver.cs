using System;
using HumanHole.Scripts.Wall;
using UnityEngine;

namespace HumanHole.Scripts
{
    public class CollisionObserver
    {
        private WallSpawner _wallSpawner;

        public event Action WallCollidedPlayer;

        public void Initial(WallSpawner wallSpawner) => 
            _wallSpawner = wallSpawner;

        public void OnEnabled()
        {
            _wallSpawner.Spawned += WallSpawned;
            _wallSpawner.Destroyed += WallDestroyed;
        }

        public void OnDisabled()
        {
            _wallSpawner.Spawned -= WallSpawned;
            _wallSpawner.Destroyed -= WallDestroyed;
        }

        private void WallSpawned(Wall.Wall wall) => 
            wall.TouchedPlayer += OnWallHitPlayer;

        private void WallDestroyed(Wall.Wall wall) => 
            wall.TouchedPlayer -= OnWallHitPlayer;

        private void OnWallHitPlayer() => 
            WallCollidedPlayer?.Invoke();
    }
}
