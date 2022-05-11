using System;
using HumanHole.Scripts.Infrastructure.Services.Ads;
using HumanHole.Scripts.UI;
using HumanHole.Scripts.UI.Panels;

namespace HumanHole.Scripts.Handlers
{
    public class AdHandler
    {
        public event Action RewardAdOpened;
        public event Action RewardAdClosed;
        public event Action RewardAdShowed;
        public event Action RewardAdOffline;
        public event Action InterstitialAdOpened;
        public event Action InterstitialAdShowed;
        public event Action InterstitialAdClosed;
        public event Action InterstitialAdOffline;

        private IAdsService _adsService;
        private LevelHandler _levelHandler;
        private WonLevelPanel _wonLevelPanel;
        private LostLevelPanel _lostLevelPanel;

        public void Initial(LevelHandler levelHandler, LevelPanelsStateMachine levelPanelsStateMachine,
            IAdsService adsService)
        {
            _levelHandler = levelHandler;
            _wonLevelPanel = levelPanelsStateMachine.GetPanel<WonLevelPanel>();
            _lostLevelPanel = levelPanelsStateMachine.GetPanel<LostLevelPanel>();
            _adsService = adsService;
        }

        public void OnEnabled()
        {
            _wonLevelPanel.RewardedAdButtonClick += OnRewardAdButtonButtonClick;
            _lostLevelPanel.RewardedAdButtonClick += OnRewardAdButtonButtonClick;
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

        public void OnDisabled()
        {
            _wonLevelPanel.RewardedAdButtonClick -= OnRewardAdButtonButtonClick;
            _lostLevelPanel.RewardedAdButtonClick -= OnRewardAdButtonButtonClick;
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

        private void OnLevelWon() =>
            _adsService.InterstitialAd.Show();

        private void OnLevelLost() =>
            _adsService.InterstitialAd.Show();

        private void OnGameStarted() =>
            _adsService.InterstitialAd.Show();

        private void OnRewardAdButtonButtonClick() =>
            _adsService.RewardedAd.Show();

        private void OnRewardedAdShowed() => 
            RewardAdShowed?.Invoke();

        private void OnRewardedAdOpened() =>
            RewardAdOpened?.Invoke();

        private void OnRewardedAdClosed() =>
            RewardAdClosed?.Invoke();

        private void OnInterstitialAdOpened() =>
            InterstitialAdOpened?.Invoke();

        private void OnInterstitialAdShowed() =>
            InterstitialAdShowed?.Invoke();

        private void OnInterstitialAdClosed() =>
            InterstitialAdClosed?.Invoke();

        private void OnInterstitialAdOffline() =>
            InterstitialAdOffline?.Invoke();

        private void OnRewardedAdOffline() =>
            RewardAdOffline?.Invoke();
    }
}