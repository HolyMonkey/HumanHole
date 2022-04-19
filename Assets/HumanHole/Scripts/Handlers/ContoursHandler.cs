using System.Collections.Generic;
using HumanHole.Scripts.Spawners;
using UnityEngine;

namespace HumanHole.Scripts.Handlers
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class ContoursHandler : MonoBehaviour
    {
        private List<Sprite> _countours;
        private SpriteRenderer _spriteRenderer;

        private WallSpawner _wallSpawner;
        private LevelHandler _levelHandler;

        public void Initial(WallSpawner wallSpawner, LevelHandler levelHandler)
        {
            _wallSpawner = wallSpawner;
            _levelHandler = levelHandler;
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void Enable() => 
            gameObject.SetActive(true);

        private void OnEnable()
        {
            _wallSpawner.Spawned += OnWallSpawned;
            _wallSpawner.Destroyed += OnWallDestroyed;
            _levelHandler.LevelLost += OnLevelLost;
        }

        private void OnDisable()
        {
            _wallSpawner.Spawned -= OnWallSpawned;
            _wallSpawner.Destroyed -= OnWallDestroyed;
            _levelHandler.LevelLost -= OnLevelLost;
        }

        private void OnLevelLost() => 
            TryToHide();

        private void OnWallSpawned(Wall wall) => 
            Show(wall);

        private void OnWallDestroyed(Wall wall) => 
            TryToHide();

        private void Show(Wall wall) => 
            _spriteRenderer.sprite = wall.Contour;

        private void TryToHide()
        {
            if (_spriteRenderer.sprite != null)
                _spriteRenderer.sprite = null;
        }
    }
}