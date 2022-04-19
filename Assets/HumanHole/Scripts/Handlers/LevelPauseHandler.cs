using System;
using HumanHole.Scripts.UI;
using UnityEngine;

namespace HumanHole.Scripts.Handlers
{
    public class LevelPauseHandler : MonoBehaviour
    {
        private AdHandler _adHandler;
        private bool _isPaused;
        private SettingsPanel _settingsPanel;
        private PlayerProfileDataPanel _playerProfileDataPanel;
        private LeaderBoardPanel _leaderBoardPanel;

        public event Action Pause;
        public event Action UnPause;

        public void Initial(AdHandler adHandler, SettingsPanel settingsPanel, PlayerProfileDataPanel playerProfileDataPanel,
            LeaderBoardPanel leaderBoardPanel)
        {
            _adHandler = adHandler;
            _settingsPanel = settingsPanel;
            _playerProfileDataPanel = playerProfileDataPanel;
            _leaderBoardPanel = leaderBoardPanel;
        }

        public void Enable() =>
            gameObject.SetActive(true);

        private void OnEnable()
        {
            _settingsPanel.Opened += OnSettingsPanelOpened;
            _settingsPanel.Closed += OnSettingsPanelClosed;
            _adHandler.RewardAdOpened += OnRewardedAdOpened;
            _adHandler.RewardAdClosed += OnRewardedAdClosed;
            _playerProfileDataPanel.Opened += OnPlayerProfileDataPanelOpened;
            _playerProfileDataPanel.Closed += OnPlayerProfileDataPanelClosed;
            _leaderBoardPanel.Opened += OnLeaderboardPanelOpened;
            _leaderBoardPanel.Closed += OnLeaderboardPanelClosed;
        }

        private void OnDisable()
        {
            _settingsPanel.Opened -= OnSettingsPanelOpened;
            _settingsPanel.Closed -= OnSettingsPanelClosed;
            _adHandler.RewardAdOpened -= OnRewardedAdOpened;
            _adHandler.RewardAdClosed -= OnRewardedAdClosed;
            _playerProfileDataPanel.Opened -= OnPlayerProfileDataPanelOpened;
            _playerProfileDataPanel.Closed -= OnPlayerProfileDataPanelClosed;
            _leaderBoardPanel.Opened -= OnLeaderboardPanelOpened;
            _leaderBoardPanel.Closed -= OnLeaderboardPanelClosed;
        }

        private void OnLeaderboardPanelOpened() =>
            OnPause();

        private void OnLeaderboardPanelClosed() =>
            OnUnpause();

        private void OnPlayerProfileDataPanelClosed() =>
            OnUnpause();

        private void OnPlayerProfileDataPanelOpened() =>
            OnPause();

        private void OnSettingsPanelClosed() =>
            OnUnpause();

        private void OnUnpause()
        {
            _isPaused = false;
            UnPause?.Invoke();
        }

        private void OnSettingsPanelOpened() =>
            OnPause();

        private void OnPause()
        {
            _isPaused = true;
            Pause?.Invoke();
        }

        private void OnRewardedAdOpened() =>
            OnPause();

        private void OnRewardedAdClosed() =>
            OnUnpause();
    }
}