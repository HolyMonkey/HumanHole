using System;
using UnityEngine;

public class AdHandler : MonoBehaviour
{
    private IAdsService _adsService;
    private IRewardService _rewardService;

    [SerializeField] private LevelHandler _levelHandler;
    [SerializeField] private LevelUI _levelUI;

    public event Action RewardAdCompleted;
    
    public void Initial()
    {
        var services = Game.Instance.AllServices;
        _adsService = services.Single<IAdsService>();
        _rewardService = services.Single<IRewardService>();
    }

    public void Enable()
    {
        gameObject.SetActive(true);
    }

    private void OnEnable()
    {
        _levelUI.ShowRewardAdButtonClick += OnShowRewardAdButtonClick;
        _levelHandler.GameStarted += OnGameStarted;
        _levelHandler.LevelLost += OnLevelLost;
        _levelHandler.LevelWon += OnLevelWon;
        _adsService.RewardedAd.Showed += OnRewardedAdShowed;
    }

    private void OnDisable()
    {
        _levelUI.ShowRewardAdButtonClick -= OnShowRewardAdButtonClick;
        _levelHandler.GameStarted -= OnGameStarted;
        _levelHandler.LevelLost -= OnLevelLost;
        _levelHandler.LevelWon -= OnLevelWon;
        _adsService.RewardedAd.Showed -= OnRewardedAdShowed;
    }

    private void OnLevelWon()
    {
        _adsService.InterstitialAd.Show();
    }

    private void OnLevelLost()
    {
        _adsService.InterstitialAd.Show();
    }

    private void OnGameStarted()
    {
        _adsService.InterstitialAd.Show();
    }

    private void OnShowRewardAdButtonClick()
    {
        _adsService.RewardedAd.Show();
    }

    private void OnRewardedAdShowed()
    {
        _rewardService.AddReward();
        RewardAdCompleted?.Invoke();
    }
}
