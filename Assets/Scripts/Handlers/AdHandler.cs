using System;
using UnityEngine;

public class AdHandler : MonoBehaviour
{
    private IAdsService _adsService;
    private IRewardService _rewardService;
    private LevelHandler _levelHandler;
    private LevelUI _levelUI;

    public event Action RewardAdOpened;
    public event Action RewardAdClosed;
    public event Action<int> RewardAdShowed;
    public event Action RewardAdOffline;

    public event Action InterstitialAdOpened;
    public event Action InterstitialAdShowed;
    public event Action InterstitialAdClosed;
    public event Action InterstitialAdOffline;
    
    public void Initial(LevelHandler levelHandler, LevelUI levelUI, IAdsService adsService, IRewardService rewardService)
    {
        _levelHandler = levelHandler;
        _levelUI = levelUI;
        _adsService = adsService;
        _rewardService = rewardService;
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
        _adsService.RewardedAd.Opened += OnRewardedAdOpened;
        _adsService.RewardedAd.Closed += OnRewardedAdClosed;
        _adsService.RewardedAd.Offline += OnRewardedAdOffline;
        _adsService.InterstitialAd.Opened += OnInterstitialAdOpened;
        _adsService.InterstitialAd.Showed += OnInterstitialAdShowed;
        _adsService.InterstitialAd.Closed += OnInterstitialAdClosed;
        _adsService.InterstitialAd.Offline += OnInterstitialAdOffline;
    }

    private void OnDisable()
    {
        _levelUI.ShowRewardAdButtonClick -= OnShowRewardAdButtonClick;
        _levelHandler.GameStarted -= OnGameStarted;
        _levelHandler.LevelLost -= OnLevelLost;
        _levelHandler.LevelWon -= OnLevelWon;
        _adsService.RewardedAd.Showed -= OnRewardedAdShowed;
        _adsService.RewardedAd.Opened -= OnRewardedAdOpened;
        _adsService.RewardedAd.Closed -= OnRewardedAdClosed;
        _adsService.RewardedAd.Offline += OnRewardedAdOffline;
        _adsService.InterstitialAd.Opened -= OnInterstitialAdOpened;
        _adsService.InterstitialAd.Showed -= OnInterstitialAdShowed;
        _adsService.InterstitialAd.Closed -= OnInterstitialAdClosed;
        _adsService.InterstitialAd.Offline -= OnInterstitialAdOffline;
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
        RewardAdShowed?.Invoke(_rewardService.RewardedPoints);
    }

    private void OnRewardedAdOpened()
    {
        RewardAdOpened?.Invoke();
    }

    private void OnRewardedAdClosed()
    {
        RewardAdClosed?.Invoke();
    }

    private void OnInterstitialAdOpened()
    {
        InterstitialAdOpened?.Invoke();
    }

    private void OnInterstitialAdShowed()
    {
        InterstitialAdShowed?.Invoke();
    }
    
    private void OnInterstitialAdClosed()
    {
        InterstitialAdClosed?.Invoke();   
    }
    
    private void OnInterstitialAdOffline()
    {
        InterstitialAdOffline?.Invoke();
    }
    
    private void OnRewardedAdOffline()
    {
        RewardAdOffline?.Invoke();
    }
}
