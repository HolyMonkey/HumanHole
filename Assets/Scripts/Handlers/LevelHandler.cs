using System;
using CodeBase.Infrastructure.Services.LeaderBoard;
using UnityEngine;
using YandexGames;

public class LevelHandler : MonoBehaviour
{
    private GameStateMachine _gameStateMachine;
    private Progress _gameProgress;
    private ISaveLoadService _saveLoadService;
    private ILeaderBoardService _leaderBoardService;
    private bool _isCollidedPlayer;
    private int _levelPoints;

    [SerializeField] private WallSpawner _wallSpawner;
    [SerializeField] private CollisionObserver _collisionObserver;
    [SerializeField] private LevelPanelsStateMachine _levelPanelsStateMachine;
    [SerializeField] private LevelUI _levelUI;
    [SerializeField] private WaterCollider _waterCollider;
    
    public bool IsLevelStarted { get; private set; }
    
    public event Action LevelStarted;
    public event Action LevelWon;
    public event Action LevelLost;
    public event Action GameStarted;

    public void Initial()
    {
        var game = Game.Instance;
        var services = game.AllServices;
        _gameStateMachine = game.StateMachine;
        _gameProgress = services.Single<IPersistentProgressService>().Progress;
        _saveLoadService = services.Single<ISaveLoadService>();
        _leaderBoardService = services.Single<ILeaderBoardService>();
    }

    public void Enable()
    {
        gameObject.SetActive(true);
    }

    private void OnEnable()
    {
        _collisionObserver.WallCollidedPlayer += OnWallCollidedPlayer;
        _waterCollider.CollidedPlayer += OnWaterCollidedPlayer;
    }

    private void Start()
    {
        if (_gameProgress.IsGameStarted)
        {
            Run();
            LevelStarted?.Invoke();
        }
        else
        {
            _levelPanelsStateMachine.SwitchPanel<StartLevelPanel>();
            _levelPanelsStateMachine.CurrentPanel.Clicked += OnStartLevelPanelClicked;
            GameStarted?.Invoke();
        }
    }

    private void OnStartLevelPanelClicked()
    {
        _levelPanelsStateMachine.CurrentPanel.Clicked -= OnStartLevelPanelClicked;
        _gameProgress.IsGameStarted = true;
        Run();
        LevelStarted?.Invoke();
    }

    private void Run()
    {
        _wallSpawner.Spawned += OnWallSpawned;
        _wallSpawner.StartSpawn();
        _wallSpawner.AllWallsDestroyed += OnAllWallsDestroyed;
        IsLevelStarted = true;
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
        if (!_isCollidedPlayer)
        {
            _levelPoints++;
            _levelUI.SetPoints(_levelPoints);
        }
    }

    private void OnAllWallsDestroyed()
    {
        _wallSpawner.AllWallsDestroyed -= OnAllWallsDestroyed;
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
        _leaderBoardService.GetPlayerEntrySuccess += OnGetPlayerEntrySuccess;
        _leaderBoardService.GetLeaderBoardPlayer();
        LevelWon?.Invoke();
    }

    private void OnLevelLost()
    {
        _isCollidedPlayer = true;
        LevelLost?.Invoke();
    }
    
    private void OnGetPlayerEntrySuccess(LeaderboardEntryResponse result)
    {
        _leaderBoardService.GetPlayerEntrySuccess -= OnGetPlayerEntrySuccess;
        if (result == null)
        {
            Debug.Log("Player is not present in the leaderboard.");
        }
        else
        {
            _leaderBoardService.SetLeaderBoardScore(_levelPoints);
        }
    }
}
