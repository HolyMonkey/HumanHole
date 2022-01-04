using System;
using UnityEngine;
using UnityEngine.Advertisements;

namespace CodeBase.Infrastructure.Services.Ads
{
    public class AdsService : IAdsService/*, IUnityAdsListener*/
    {
        private const string AndroidGameId = "4048439";
        private const string IOSGameId = "4048438";

        private const string RewardedVideoPlacementId = "rewardedVideo";

        private readonly string _gameId;
        private Action _onVideoFinished;
        
        public int Reward => 10;
        
        public event Action RewardedVideoReady;


        public AdsService()
        {
            switch (Application.platform)
            {
                case RuntimePlatform.Android:
                    _gameId = AndroidGameId;
                    break;
                case RuntimePlatform.IPhonePlayer:
                    _gameId = IOSGameId;
                    break;
                case RuntimePlatform.WindowsEditor:
                    _gameId = AndroidGameId;
                    break;
                default:
                    Debug.Log("Unsupported platform for ads");
                    break;
            }

            /*Advertisement.AddListener(this);
            Advertisement.Initialize(_gameId);*/
        }

        public void ShowRewardedVideo(Action onVideoFinished)
        {
            /*Advertisement.Show(RewardedVideoPlacementId);*/
            _onVideoFinished = onVideoFinished;
        }

        public bool IsRewardedVideoReady =>
            /*get { return Advertisement.IsReady(RewardedVideoPlacementId); }*/
            false;

        public void OnUnityAdsReady(string placementId)
        {
            Debug.Log($"OnUnityAdsReady {placementId}");

            if (placementId == RewardedVideoPlacementId) 
                RewardedVideoReady?.Invoke();
        }
        
        public void OnUnityAdsDidError(string message) => 
            Debug.Log($"OnUnityAdsDidError {message}");

        public void OnUnityAdsDidStart(string placementId) => 
            Debug.Log($"OnUnityAdsDidStart {placementId}");

        /*public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
        {
            switch (showResult)
            {
                case ShowResult.Failed:
                    Debug.LogError($"OnUnityAdsDidFinish {showResult}");
                    break;
                case ShowResult.Skipped:
                    Debug.LogError($"OnUnityAdsDidFinish {showResult}");
                    break;
                case ShowResult.Finished:
                    _onVideoFinished?.Invoke();
                    break;
            }

            _onVideoFinished = null;
        }*/
    }
}