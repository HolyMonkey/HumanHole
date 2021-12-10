using System;
using UnityEngine;
using Zenject;

public class GameRunner : MonoBehaviour
{
    private int _index;
    
    [SerializeField] private LevelConfig _levelConfig;
    [SerializeField] private WallSpawner _wallSpawner;
    [SerializeField] private Contours _contours;
    
    private GameData _gameData;
    private StartGameTapPanel _startGameTapPanel;

    public bool IsGameStarted { get; private set; }
    
    public event Action GameStarted;
    public event Action GameFinished;

    [Inject]
    private void Container(GameData gameData, StartGameTapPanel startGameTapPanel)
    {
        _startGameTapPanel = startGameTapPanel;
        _gameData = gameData;
    }

    private void Start()
    {
        Cursor.visible = false;
        
        if (_gameData.IsGameStarted)
        {
            Run();
        }
        else
        {
            _startGameTapPanel.Enable();
            _startGameTapPanel.Clicked += OnStartGameTapPanelClicked;
        }
    }

    private void OnStartGameTapPanelClicked()
    {
        _startGameTapPanel.Clicked -= OnStartGameTapPanelClicked;
        _startGameTapPanel.Disable();
        _gameData.IsGameStarted = true;
        Run();
    }

    private void Run()
    {
        _wallSpawner.Initialize(_levelConfig.Walls, _levelConfig.WallsSpeed);
        _contours.Initialize(_levelConfig.Countours);
        _wallSpawner.StartSpawn();
        IsGameStarted = true;
        GameStarted?.Invoke();
    }
}