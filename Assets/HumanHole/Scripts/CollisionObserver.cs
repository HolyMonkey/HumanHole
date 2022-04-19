using System;
using HumanHole.Scripts.Spawners;
using UnityEngine;

namespace HumanHole.Scripts
{
    public class CollisionObserver : MonoBehaviour
    {
        private bool _touchedPerson;
        private WallSpawner _wallSpawner;

        public event Action WallCollidedPlayer;

        public void Initial(WallSpawner wallSpawner) => 
            _wallSpawner = wallSpawner;

        public void Enable() => 
            gameObject.SetActive(true);

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
            wall.TouchedPlayer += OnWallHitPlayer;
        }

        private void WallDestroyed(Wall wall) => 
            wall.TouchedPlayer -= OnWallHitPlayer;

        private void OnWallHitPlayer()
        {
            _touchedPerson = true;
            WallCollidedPlayer?.Invoke();
        }
    }
}
