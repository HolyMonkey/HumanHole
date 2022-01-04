using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Contours : MonoBehaviour
{
    private List<Sprite> _countours;
    private SpriteRenderer _spriteRenderer;

    [SerializeField] private WallSpawner _wallSpawner;
    [SerializeField] private LevelHandler _levelHandler;
    
    public void Initial()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Enable()
    {
        gameObject.SetActive(true);
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }
    
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
    
    private void OnLevelLost()
    {
        Hide();
    }

    private void OnWallSpawned(Wall wall)
    {
        Show(wall);
    }

    private void OnWallDestroyed(Wall wall)
    {
        Hide();
    }

    private void Show(Wall wall)
    {
        _spriteRenderer.sprite = wall.Contour;
    }

    private void Hide()
    {
        _spriteRenderer.sprite = null;
    }
}
