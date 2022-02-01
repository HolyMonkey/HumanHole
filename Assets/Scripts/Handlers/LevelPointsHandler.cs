using UnityEngine;

public class LevelPointsHandler : MonoBehaviour
{
    private WallSpawner _wallSpawner;
    private LevelUI _levelUI;
    private LevelHandler _levelHandler;
    private Progress _progress;
    private ISaveLoadService _saveLoadService;
    private AdHandler _adHandler;

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
    }
    
    private void OnEnable()
    {
        _wallSpawner.Spawned += OnWallSpawned;
        _levelHandler.LevelWon += OnLevelWon;
        _adHandler.RewardAdShowed += OnRewardedAdShowed;
    }

    private void OnRewardedAdShowed(int rewardedPoints)
    {
        _levelUI.SetPoints(LevelPoints + rewardedPoints);
    }

    private void OnDisable()
    {
        _wallSpawner.Spawned -= OnWallSpawned;
        _levelHandler.LevelWon -= OnLevelWon;
    }

    private void OnLevelWon()
    {
        _progress.UpdatePoints(LevelPoints);
        _saveLoadService.SaveProgress();
    }

    private void OnWallSpawned(Wall wall)
    {
        _wallSpawner.LeftPlayerZone += OnWallLeftPlayerZone;
    }
    
    private void OnWallLeftPlayerZone(Wall wall)
    {
        _wallSpawner.LeftPlayerZone -= OnWallLeftPlayerZone;
        if (!_levelHandler.IsLevelLost)
        {
            LevelPoints++;
            _levelUI.SetPoints(LevelPoints);
        }
    }
}
