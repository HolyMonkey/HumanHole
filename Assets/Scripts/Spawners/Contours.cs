using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Contours : MonoBehaviour
{
    private int _index;
    private List<Sprite> _countours;
    private SpriteRenderer _spriteRenderer;
    [SerializeField] private WallSpawner _wallSpawner;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Initialize(List<Sprite> countours)
    {
        _countours = countours;
    }

    private void OnEnable()
    {
        _wallSpawner.Spawned += OnWallSpawned;
        _wallSpawner.Destroyed += OnWallDestroyed;
    }

    private void OnDisable()
    {
        _wallSpawner.Spawned -= OnWallSpawned;
        _wallSpawner.Destroyed -= OnWallDestroyed;
    }

    private void OnWallSpawned()
    {
        Show();
    }

    private void OnWallDestroyed()
    {
        Hide();
    }

    private void Show()
    {
        _spriteRenderer.sprite = _countours[_index];
        _index++;
    }

    private void Hide()
    {
        _spriteRenderer.sprite = null;
    }
}
