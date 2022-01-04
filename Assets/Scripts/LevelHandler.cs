using System;
using UnityEngine;

public class LevelHandler : MonoBehaviour
{
    private GameStateMachine _gameStateMachine;
    private Progress _gameProgress;
    private ISaveLoadService _saveLoadService;
    private bool _isCollidedPlayer;
    private int _levelPoints;

    [SerializeField] private WallSpawner _wallSpawner;
    [SerializeField] private CollisionObserver _collisionObserver;
    [SerializeField] private LevelPanelsStateMachine _levelPanelsStateMachine;
    [SerializeField] private LevelUI _levelUI;
    [SerializeField] private WaterCollider _waterCollider;
    
    public bool IsGameStarted { get; private set; }
    
    public event Action LevelStarted;
    public event Action LevelWon;
    public event Action LevelLost;

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
        _collisionObserver.WallCollidedPlayer += OnWallCollidedPlayer;
        _waterCollider.CollidedPlayer += OnWaterCollidedPlayer;
    }

    public void Initial()
    {
        var game = Game.Instance;
        _gameProgress = game.AllServices.Single<IPersistentProgressService>().Progress;
        _gameStateMachine = game.StateMachine;
        _saveLoadService = game.AllServices.Single<ISaveLoadService>();
        
        if (_gameProgress.IsGameStarted)
        {
            Run();
        }
        else
        {
            _levelPanelsStateMachine.SwitchPanel<StartLevelPanel>();
            _levelPanelsStateMachine.CurrentPanel.Clicked += OnStartLevelPanelClicked;
        }
    }

    private void OnStartLevelPanelClicked()
    {
        _levelPanelsStateMachine.CurrentPanel.Clicked -= OnStartLevelPanelClicked;
        _gameProgress.IsGameStarted = true;
        Run();
    }

    private void Run()
    {
        _wallSpawner.Spawned += OnWallSpawned;
        _wallSpawner.StartSpawn();
        _wallSpawner.AllWallsDestroyed += OnAllWallsDestroyed;
        IsGameStarted = true;
        LevelStarted?.Invoke();
    }

    private void OnWaterCollidedPlayer()
    {
        if (!_isCollidedPlayer)
        {
            _collisionObserver.WallCollidedPlayer -= OnWallCollidedPlayer;
            _levelPanelsStateMachine.SwitchPanel<LostLevelPanel>();
            _levelPanelsStateMachine.CurrentPanel.Clicked += OnLostLevelPanelClicked;
            OnLevelLost();
        }
    }

    private void OnWallSpawned(Wall wall)
    {
        _wallSpawner.LeftPlayerZone += OnWallLeftPlayerZone;
    }

    private void OnWallLeftPlayerZone(Wall wall)
    {
        _wallSpawner.LeftPlayerZone -= OnWallLeftPlayerZone;
        Debug.Log("OnWallLeftPlayerZone");
        if (!_isCollidedPlayer)
        {
            _levelPoints++;
            _levelUI.SetPoints(_levelPoints);
        }
    }

    private void OnAllWallsDestroyed()
    {
        _wallSpawner.AllWallsDestroyed -= OnAllWallsDestroyed;
        Debug.Log("OnAllWallsDestroyed");
        if (!_isCollidedPlayer)
        {
            _levelPanelsStateMachine.SwitchPanel<WonLevelPanel>();
            _levelPanelsStateMachine.CurrentPanel.Clicked += OnWonLevelPanelClicked;
            OnLevelWon();
        }
    }

    private void OnWonLevelPanelClicked()
    {
        _levelPanelsStateMachine.CurrentPanel.Clicked -= OnWonLevelPanelClicked;
        _gameStateMachine.Enter<LoadLevelState, string, bool>(_gameProgress.LevelName(), false);
    }


    private void OnWallCollidedPlayer()
    {
        if (!_isCollidedPlayer)
        {
            _collisionObserver.WallCollidedPlayer -= OnWallCollidedPlayer;
            _levelPanelsStateMachine.SwitchPanel<LostLevelPanel>();
            _levelPanelsStateMachine.CurrentPanel.Clicked += OnLostLevelPanelClicked;
            OnLevelLost();
        }
    }

    private void OnLostLevelPanelClicked()
    {
        _levelPanelsStateMachine.CurrentPanel.Clicked -= OnLostLevelPanelClicked;
        _gameStateMachine.Enter<LoadLevelState, string, bool>(_gameProgress.LevelName(), true);
    }

    private void OnLevelWon()
    {
        _gameProgress.UpdateLevel();
        _gameProgress.UpdatePoints(_levelPoints);
        _saveLoadService.SaveProgress();
        LevelWon?.Invoke();
        Debug.Log("WonLevel");
    }

    private void OnLevelLost()
    {
        Debug.Log("LostLevel");
        _isCollidedPlayer = true;
        LevelLost?.Invoke();
    }
}
