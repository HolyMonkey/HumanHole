using HumanHole.Scripts.Data;
using HumanHole.Scripts.Infrastructure.Services.Analytics;
using UnityEngine;

namespace HumanHole.Scripts.Handlers
{
    public class AnalyticsHandler : MonoBehaviour
    {
        private const string Key = "Number";
        private IAnalyticsService _analyticsService;
        private LevelHandler _levelHandler;
        private AdHandler _adHandler;
        private Progress _progress;

        public void Initial(LevelHandler levelHandler, AdHandler adHandler, IAnalyticsService analyticsService, Progress progress)
        {
            _progress = progress;
            _adHandler = adHandler;
            _levelHandler = levelHandler;
            _analyticsService = analyticsService;
        }

        public void Enable() => 
            gameObject.SetActive(true);

        private void OnEnable()
        {
            _levelHandler.LevelStarted += OnLevelStarted;
            _levelHandler.LevelWon += OnLevelWon;
            _levelHandler.LevelLost += OnLevelLost;
            _adHandler.RewardAdOpened += OnRewardAdOpened;
            _adHandler.RewardAdClosed += OnRewardAdClosed;
            _adHandler.RewardAdShowed += OnRewardAdShowed;
            _adHandler.RewardAdOffline += OnRewardAdOffline;
            _adHandler.InterstitialAdOpened += OnInterstitialAdOpened;
            _adHandler.InterstitialAdShowed += OnInterstitialAdShowed;
            _adHandler.InterstitialAdClosed += OnInterstitialAdClosed;
            _adHandler.InterstitialAdOffline += OnInterstitialAdOffline;
        }

        private void OnDisable()
        {
            _levelHandler.LevelStarted -= OnLevelStarted;
            _levelHandler.LevelWon -= OnLevelWon;
            _levelHandler.LevelLost -= OnLevelLost;
            _adHandler.RewardAdOpened -= OnRewardAdOpened;
            _adHandler.RewardAdClosed -= OnRewardAdClosed;
            _adHandler.RewardAdShowed -= OnRewardAdShowed;
            _adHandler.RewardAdOffline -= OnRewardAdOffline;
            _adHandler.InterstitialAdOpened -= OnInterstitialAdOpened;
            _adHandler.InterstitialAdShowed -= OnInterstitialAdShowed;
            _adHandler.InterstitialAdClosed -= OnInterstitialAdClosed;
            _adHandler.InterstitialAdOffline -= OnInterstitialAdOffline;  
        }
        
        private void OnInterstitialAdOffline() => 
            SetEvent(AnalyticsEvents.InterstitialAdOffline);

        private void OnInterstitialAdClosed() => 
            SetEvent(AnalyticsEvents.InterstitialAdClose);

        private void OnInterstitialAdShowed() => 
            SetEvent(AnalyticsEvents.InterstitialAdShow);

        private void OnInterstitialAdOpened() => 
            SetEvent(AnalyticsEvents.InterstitialAdOpen);

        private void OnRewardAdOffline() => 
            SetEvent(AnalyticsEvents.RewardedAdOffline);

        private void OnRewardAdShowed(int obj) => 
            SetEvent(AnalyticsEvents.RewardedAdAdShow);

        private void OnRewardAdClosed() => 
            SetEvent(AnalyticsEvents.RewardedAdClosed);

        private void OnRewardAdOpened() => 
            SetEvent(AnalyticsEvents.RewardedAdAdOpen);

        private void OnLevelWon() => 
            SetEvent(AnalyticsEvents.LevelComplete, Key, _progress.LevelNumber);

        private void OnLevelStarted() => 
            SetEvent(AnalyticsEvents.LevelStart, Key, _progress.LevelNumber);

        private void OnLevelLost() => 
            SetEvent(AnalyticsEvents.LevelFail, Key, _progress.LevelNumber);

        private void SetEvent(AnalyticsEvents @event, string key, object value) => 
            _analyticsService.SetEvent(@event, key, value);

        private void SetEvent(AnalyticsEvents @event) => 
            _analyticsService.SetEvent(@event);
    }
}
