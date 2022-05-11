using System;
using HumanHole.Scripts.Data;
using HumanHole.Scripts.Infrastructure.Services.SaveLoad;
using HumanHole.Scripts.Infrastructure.States;
using HumanHole.Scripts.Player;
using HumanHole.Scripts.UI;
using HumanHole.Scripts.UI.Panels;
using HumanHole.Scripts.Wall;
using HumanHole.Scripts.WaterLogic;

namespace HumanHole.Scripts.Handlers
{
    public class LevelHandler
    {
        public bool IsLevelStarted { get; private set; }
        public bool IsLevelLost { get; private set; }

        public event Action LevelStarted;
        public event Action LevelWon;
        public event Action LevelLost;
        public event Action GameStarted;

        private GameStateMachine _gameStateMachine;
        private LevelsProgress _levelsProgress;
        private ISaveLoadService _saveLoadService;
        private WallSpawner _wallSpawner;
        private CollisionObserver _collisionObserver;
        private LevelPanelsStateMachine _levelPanelsStateMachine;
        private WaterCollider _waterCollider;
        private CharacterSpawner _characterSpawner;
        private StartLevelPanel _startLevelPanel;
        private WonLevelPanel _wonLevelPanel;
        private LostLevelPanel _lostLevelPanel;
        private LevelUI _levelUI;

        private bool _wonLevel;

        public void Initial(GameStateMachine gameStateMachine, Progress progress, ISaveLoadService saveLoadService,
            WallSpawner wallSpawner, CollisionObserver collisionObserver,
            LevelPanelsStateMachine levelPanelsStateMachine, WaterCollider waterCollider,
            CharacterSpawner characterSpawner, LevelUI levelUI)
        {
            _levelUI = levelUI;
            _characterSpawner = characterSpawner;
            _gameStateMachine = gameStateMachine;
            _levelsProgress = progress.LevelsProgress;
            _saveLoadService = saveLoadService;
            _wallSpawner = wallSpawner;
            _collisionObserver = collisionObserver;
            _levelPanelsStateMachine = levelPanelsStateMachine;
            _waterCollider = waterCollider;
            _wonLevelPanel = _levelPanelsStateMachine.GetPanel<WonLevelPanel>();
            _lostLevelPanel = _levelPanelsStateMachine.GetPanel<LostLevelPanel>();
            _startLevelPanel = _levelPanelsStateMachine.GetPanel<StartLevelPanel>();
        }

        public void OnEnabled()
        {
            _collisionObserver.WallCollidedPlayer += OnWallCollidedPlayer;
            _waterCollider.CollidedPlayer += OnWaterColliderCollidedPlayer;
            _wonLevelPanel.Closed += OnWonLevelPanelClosed;
            _wonLevelPanel.RestartButtonClick += OnWonLevelPanelRestartButtonClicked;
            _lostLevelPanel.RestartButtonClick += OnLostLevelPanelRestartButtonClicked;
            _lostLevelPanel.Closed += OnLostLevelPanelClosed;
            _wallSpawner.AllWallsDestroyed += OnAllWallsDestroyed;
            _startLevelPanel.Clicked += OnStartLevelPanelClosed;
        }

        public void OnDisabled()
        {
            _wonLevelPanel.Closed -= OnWonLevelPanelClosed;
            ;
            _lostLevelPanel.Closed -= OnLostLevelPanelClosed;
            _wonLevelPanel.RestartButtonClick -= OnWonLevelPanelRestartButtonClicked;
            _lostLevelPanel.RestartButtonClick -= OnLostLevelPanelRestartButtonClicked;
            _startLevelPanel.Clicked -= OnStartLevelPanelClosed;
        }

        public void OnStarted()
        {
            _levelPanelsStateMachine.SetPanel<StartLevelPanel>();
            GameStarted?.Invoke();
        }

        private void OnLostLevelPanelClosed() =>
            LoadLevel();

        private void OnWonLevelPanelRestartButtonClicked()
        {
            _levelsProgress.TryDecreaseLevelNumber();
            SaveProgress();
            LoadLevel();
        }

        private void OnLostLevelPanelRestartButtonClicked()
        {
            SaveProgress();
            LoadLevel();
        }

        private void OnWonLevelPanelClosed() =>
            LoadLevel();

        private void OnStartLevelPanelClosed() =>
            Run();

        private void Run()
        {
            _wallSpawner.StartSpawn();
            _characterSpawner.Spawn();
            IsLevelStarted = true;
            LevelStarted?.Invoke();
        }

        private void OnWaterColliderCollidedPlayer()
        {
            if (!IsLevelLost)
            {
                _collisionObserver.WallCollidedPlayer -= OnWallCollidedPlayer;
                _levelPanelsStateMachine.SetPanel<LostLevelPanel>();
                OnLevelLost();
            }
        }

        private void OnAllWallsDestroyed()
        {
            _wallSpawner.AllWallsDestroyed -= OnAllWallsDestroyed;
            if (!IsLevelLost)
            {
                _levelPanelsStateMachine.SetPanel<WonLevelPanel>();
                OnLevelWon();
            }
            else
            {
                _levelPanelsStateMachine.SetPanel<LostLevelPanel>();
            }
        }

        private void OnWallCollidedPlayer()
        {
            if (!IsLevelLost)
            {
                _collisionObserver.WallCollidedPlayer -= OnWallCollidedPlayer;
                OnLevelLost();
            }
        }

        private void LoadLevel() =>
            _gameStateMachine.Enter<LoadLevelState, string, bool>(_levelsProgress.LevelName(), true);

        private void SaveProgress() =>
            _saveLoadService.SaveProgress();

        private void OnLevelWon()
        {
            _wonLevel = true;
            DisableUI();
            UpdateAttempts();
            _levelsProgress.TryIncreaseLevelNumber();
            SaveProgress();
            LevelWon?.Invoke();
        }

        private void OnLevelLost()
        {
            DisableUI();
            IsLevelLost = true;
            UpdateAttempts();
            SaveProgress();
            LevelLost?.Invoke();
        }

        private void DisableUI() =>
            _levelUI.DisableUI();

        private void UpdateAttempts()
        {
            _levelsProgress.AddAttempt();
            int attempts = _levelsProgress.Attempts;
            if (_wonLevel)
                _wonLevelPanel.SetAttempts(attempts);
            else
                _lostLevelPanel.SetAttempts(attempts);
        }
    }
}