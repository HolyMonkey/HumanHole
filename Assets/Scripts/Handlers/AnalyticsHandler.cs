using Infrastructure.Services.Analytics;
using UnityEngine;

namespace Handlers
{
    public class AnalyticsHandler : MonoBehaviour
    {
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

        public void Enable()
        {
            gameObject.SetActive(true);
        }

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
            _adHandler.InterstitialAdOffline += OnInterstitialAdOfflined;
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
            _adHandler.InterstitialAdOffline -= OnInterstitialAdOfflined;  
        }
        
        private void OnInterstitialAdOfflined()
        {
            SetEvent(AnalyticsEvents.InterstitialAdOffline);
        }

        private void OnInterstitialAdClosed()
        {
            SetEvent(AnalyticsEvents.InterstitialAdClose);
        }

        private void OnInterstitialAdShowed()
        {
            SetEvent(AnalyticsEvents.InterstitialAdShow);
        }

        private void OnInterstitialAdOpened()
        {
            SetEvent(AnalyticsEvents.InterstitialAdOpen);
        }

        private void OnRewardAdOffline()
        {
            SetEvent(AnalyticsEvents.RewardedAdOffline);
        }

        private void OnRewardAdShowed(int obj)
        {
            SetEvent(AnalyticsEvents.RewardedAdAdShow);
        }

        private void OnRewardAdClosed()
        {
            SetEvent(AnalyticsEvents.RewardedAdClosed);
        }

        private void OnRewardAdOpened()
        {
            SetEvent(AnalyticsEvents.RewardedAdAdOpen);
        }

        private void OnLevelWon()
        {
            SetEvent(AnalyticsEvents.LevelComplete, "Number", _progress.LevelNumber);
        }

        private void OnLevelStarted()
        {
            SetEvent(AnalyticsEvents.LevelStart, "Number", _progress.LevelNumber);
        }

        private void OnLevelLost()
        {
            SetEvent(AnalyticsEvents.LevelFail, "Number", _progress.LevelNumber);
        }

        private void SetEvent(AnalyticsEvents @event, string key, object value)
        {
            _analyticsService.SetEvent(@event, key, value);
        }
        
        private void SetEvent(AnalyticsEvents @event)
        {
            _analyticsService.SetEvent(@event);
        }
    }
}