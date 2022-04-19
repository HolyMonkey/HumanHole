using HumanHole.Scripts.Data;
using HumanHole.Scripts.Infrastructure.Services.SaveLoad;
using HumanHole.Scripts.Spawners;
using HumanHole.Scripts.UI;
using UnityEngine;

namespace HumanHole.Scripts.Handlers
{
    public class LevelPointsHandler : MonoBehaviour
    {
        private WallSpawner _wallSpawner;
        private LevelUI _levelUI;
        private LevelHandler _levelHandler;
        private Progress _progress;
        private ISaveLoadService _saveLoadService;
        private AdHandler _adHandler;
        private int _rewardedPoints;
        private int _progressPoints;
    
        public int LevelPoints { get; private set; }

        public void Initial(WallSpawner wallSpawner, LevelUI levelUI, LevelHandler levelHandler, Progress progress, ISaveLoadService saveLoadService, AdHandler adHandler)
        {
            _adHandler = adHandler;
            _saveLoadService = saveLoadService;
            _progress = progress;
            _levelHandler = levelHandler;
            _levelUI = levelUI;
            _wallSpawner = wallSpawner;
        }

        public void Enable()
        {
            gameObject.SetActive(true);
            _progressPoints = _progress.Points;
            SetPoints();
        }
    
        private void OnEnable()
        {
            _wallSpawner.Spawned += OnWallSpawned;
            _levelHandler.LevelWon += OnLevelWon;
            _adHandler.RewardAdShowed += OnRewardedAdShowed;
        }

        private void OnDisable()
        {
            _wallSpawner.Spawned -= OnWallSpawned;
            _levelHandler.LevelWon -= OnLevelWon;
            _adHandler.RewardAdShowed -= OnRewardedAdShowed;
        }

        private void OnRewardedAdShowed(int rewardedPoints)
        {
            _rewardedPoints += rewardedPoints;
            SetPoints();
        }

        private void SetPoints() => 
            _levelUI.SetPoints(LevelPoints + _rewardedPoints + _progressPoints);

        private void OnLevelWon() => 
            UpdatePoints(LevelPoints);

        private void UpdatePoints(int points)
        {
            _progress.UpdatePoints(points);
            _saveLoadService.SaveProgress();
        }

        private void OnWallSpawned(Wall wall) => 
            _wallSpawner.LeftPlayerZone += OnWallLeftPlayerZone;

        private void OnWallLeftPlayerZone(Wall wall)
        {
            _wallSpawner.LeftPlayerZone -= OnWallLeftPlayerZone;
            if (!_levelHandler.IsLevelLost)
            {
                LevelPoints++;
                SetPoints();
            }
        }
    }
}
