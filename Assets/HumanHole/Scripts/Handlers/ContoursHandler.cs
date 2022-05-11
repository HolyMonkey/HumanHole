using System.Collections.Generic;
using HumanHole.Scripts.Wall;
using UnityEngine;

namespace HumanHole.Scripts.Handlers
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class ContoursHandler : MonoBehaviour
    {
        [SerializeField] private Vector3 _originPosition;
        
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

        public void OnEnabled()
        {
            gameObject.SetActive(true);
            _wallSpawner.Spawned += OnWallSpawned;
            _wallSpawner.Destroyed += OnWallDestroyed;
            _levelHandler.LevelLost += OnLevelLost;
        }

        public void OnDisabled()
        {
            _wallSpawner.Spawned -= OnWallSpawned;
            _wallSpawner.Destroyed -= OnWallDestroyed;
            _levelHandler.LevelLost -= OnLevelLost;
        }

        public void OnStarted()
        {
            transform.position = _originPosition;
        }
        
        private void OnLevelLost() => 
            TryToHide();

        private void OnWallSpawned(Wall.Wall wall) => 
            Show(wall);

        private void OnWallDestroyed(Wall.Wall wall) => 
            TryToHide();

        private void Show(Wall.Wall wall) => 
            _spriteRenderer.sprite = wall.Contour;

        private void TryToHide()
        {
            if (_spriteRenderer.sprite != null)
                _spriteRenderer.sprite = null;
        }
    }
}