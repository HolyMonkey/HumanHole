using System;
using UnityEngine;

public class LevelPauseHandler : MonoBehaviour
{
    private AdHandler _adHandler;
    
    [SerializeField] private SettingsPanel _settingsPanel;
    [SerializeField] private PlayerProfileDataPanel _playerProfileDataPanel;
    [SerializeField] private LeaderBoardPanel _leaderBoardPanel;

    public bool IsPaused { get; private set; }
    
    public event Action Pause;
    public event Action UnPause;
    
    public void Initial(AdHandler adHandler)
    {
        _adHandler = adHandler;
    }

    public void Enable()
    {
        gameObject.SetActive(true);
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }
    
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

    private void OnLeaderboardPanelOpened()
    {
        OnPause();
    }

    private void OnLeaderboardPanelClosed()
    {
        OnUnpause();
    }

    private void OnPlayerProfileDataPanelClosed()
    {
        OnUnpause();
    }

    private void OnPlayerProfileDataPanelOpened()
    {
        OnPause();
    }

    private void OnSettingsPanelClosed()
    {
        OnUnpause();
    }

    private void OnUnpause()
    {
        IsPaused = false;
        UnPause?.Invoke();
    }

    private void OnSettingsPanelOpened()
    {
        OnPause();
    }

    private void OnPause()
    {
        IsPaused = true;
        Pause?.Invoke();
    }

    private void OnRewardedAdOpened()
    {
        OnPause();
    }

    private void OnRewardedAdClosed()
    {
        OnUnpause();
    }
}
