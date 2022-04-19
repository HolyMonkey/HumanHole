using System;
using HumanHole.Scripts.Data;
using HumanHole.Scripts.Infrastructure.Services.SaveLoad;
using HumanHole.Scripts.Infrastructure.States;
using HumanHole.Scripts.Spawners;
using HumanHole.Scripts.UI;
using UnityEngine;

namespace HumanHole.Scripts.Handlers
{
    public class LevelHandler : MonoBehaviour
    {
        private GameStateMachine _gameStateMachine;
        private Progress _gameProgress;
        private ISaveLoadService _saveLoadService;
        private WallSpawner _wallSpawner;
        private CollisionObserver _collisionObserver;
        private LevelPanelsStateMachine _levelPanelsStateMachine;
        private WaterCollider _waterCollider;
    
        public bool IsLevelStarted { get; private set; }
        public bool IsLevelLost { get; private set; }
    
        public event Action LevelStarted;
        public event Action LevelWon;
        public event Action LevelLost;
        public event Action GameStarted;

        public void Initial(GameStateMachine gameStateMachine, Progress progress, ISaveLoadService saveLoadService, 
            WallSpawner wallSpawner, CollisionObserver collisionObserver, LevelPanelsStateMachine levelPanelsStateMachine, WaterCollider waterCollider)
        {
            _gameStateMachine = gameStateMachine;
            _gameProgress = progress;
            _saveLoadService = saveLoadService;
            _wallSpawner = wallSpawner;
            _collisionObserver = collisionObserver;
            _levelPanelsStateMachine = levelPanelsStateMachine;
            _waterCollider = waterCollider;
        }

        public void Enable() => 
            gameObject.SetActive(true);

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
            _wallSpawner.StartSpawn();
            _wallSpawner.AllWallsDestroyed += OnAllWallsDestroyed;
            IsLevelStarted = true;
        }

        private void OnWaterCollidedPlayer()
        {
            if (!IsLevelLost)
            {
                _collisionObserver.WallCollidedPlayer -= OnWallCollidedPlayer;
                _levelPanelsStateMachine.SwitchPanel<LostLevelPanel>();
                _levelPanelsStateMachine.CurrentPanel.Clicked += OnLostLevelPanelClicked;
                OnLevelLost();
            }
        }

        private void OnAllWallsDestroyed()
        {
            _wallSpawner.AllWallsDestroyed -= OnAllWallsDestroyed;
            if (!IsLevelLost)
            {
                _levelPanelsStateMachine.SwitchPanel<WonLevelPanel>();
                _levelPanelsStateMachine.CurrentPanel.Clicked += OnWonLevelPanelClicked;
                OnLevelWon();
            }
        }

        private void OnWonLevelPanelClicked()
        {
            _levelPanelsStateMachine.CurrentPanel.Clicked -= OnWonLevelPanelClicked;
            _gameStateMachine.Enter<LoadLevelState, string, bool>(_gameProgress.LevelName(), true);
        }


        private void OnWallCollidedPlayer()
        {
            if (!IsLevelLost)
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
            _gameProgress.TryUpdateLevel();
            _saveLoadService.Saved += OnProgressSaved;
            _saveLoadService.SaveProgress();
        }

        private void OnProgressSaved()
        {
            _saveLoadService.Saved -= OnProgressSaved;
            LevelWon?.Invoke();
        }

        private void OnLevelLost()
        {
            IsLevelLost = true;
            LevelLost?.Invoke();
        }
    }
}
