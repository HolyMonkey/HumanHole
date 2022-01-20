using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardService : IRewardService
{
    private const int Points = 3;
    
    private readonly IPersistentProgressService _progressService;
    private readonly ISaveLoadService _saveLoadService;

    public RewardService(IPersistentProgressService progressService, ISaveLoadService saveLoadService)
    {
        _saveLoadService = saveLoadService;
        _progressService = progressService;
    }

    public void AddReward()
    {
        _progressService.Progress.UpdatePoints(Points);
        _saveLoadService.SaveProgress();
    }
}
