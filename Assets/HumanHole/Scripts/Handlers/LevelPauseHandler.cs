using System;
using HumanHole.Scripts.UI;
using HumanHole.Scripts.UI.Panels;
using UnityEngine;

namespace HumanHole.Scripts.Handlers
{
    public class LevelPauseHandler
    {
        public bool IsPaused { get; private set; }
        public event Action Pause;
        public event Action UnPause;

        private AdHandler _adHandler;
        private LevelPanelsStateMachine _levelPanelsStateMachine;
        private LeaderBoardPanel _leaderBoardPanel;

        public void Initial(AdHandler adHandler, LevelPanelsStateMachine levelPanelsStateMachine)
        {
            _adHandler = adHandler;
            _levelPanelsStateMachine = levelPanelsStateMachine;
            _leaderBoardPanel = _levelPanelsStateMachine.GetPanel<LeaderBoardPanel>();
        }

        public void OnEnabled()
        {
            _adHandler.RewardAdOpened += OnRewardedAdOpened;
            _adHandler.RewardAdClosed += OnRewardedAdClosed;

            _leaderBoardPanel.Opened += OnLeaderboardPanelOpened;
            _leaderBoardPanel.Closed += OnLeaderboardPanelClosed;
        }

        public void OnDisabled()
        {
            _adHandler.RewardAdOpened -= OnRewardedAdOpened;
            _adHandler.RewardAdClosed -= OnRewardedAdClosed;
            _leaderBoardPanel.Opened -= OnLeaderboardPanelOpened;
            _leaderBoardPanel.Closed -= OnLeaderboardPanelClosed;
        }

        private void OnLeaderboardPanelOpened() =>
            OnPause();

        private void OnLeaderboardPanelClosed() =>
            OnUnpause();

        private void OnSettingsPanelClosed() =>
            OnUnpause();

        private void OnUnpause()
        {
            IsPaused = false;
            UnPause?.Invoke();
        }

        private void OnSettingsPanelOpened() =>
            OnPause();

        private void OnPause()
        {
            IsPaused = true;
            Pause?.Invoke();
        }

        private void OnRewardedAdOpened() =>
            OnPause();

        private void OnRewardedAdClosed() =>
            OnUnpause();
    }
}