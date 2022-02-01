using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour
{
    private LevelHandler _levelHandler;

    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private TextMeshProUGUI _pointsText;
    [SerializeField] private GameObject _balanceSlider;
    [SerializeField] private GameObject _progressSlider;
    [SerializeField] private Button _showRewarButton;
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _playerProfileButton;
    [SerializeField] private Button _leaderBoardButton;

    [SerializeField] private SettingsPanel _settingsPanel;
    [SerializeField] private PlayerProfileDataPanel _playerProfileDataPanel;
    [SerializeField] private LeaderBoardPanel _leaderBoardPanel;

    public event Action ShowRewardAdButtonClick;

    public void Initial(LevelHandler levelHandler, Progress progress)
    {
        _levelHandler = levelHandler;
        
        SetLevelName(progress.LevelNumber);
        SetPoints(progress.Points);
        
        _showRewarButton.onClick.AddListener(()=>ShowRewardAdButtonClick?.Invoke());
        _playerProfileButton.onClick.AddListener(OnPlayerProfilePanelButtonClick);
        _settingsButton.onClick.AddListener(OnSettingsPanelButtonClick);
        _levelHandler.LevelLost += OnLevelCompleted;
        _levelHandler.LevelWon += OnLevelCompleted;
        
        _settingsPanel.Initial();

        _leaderBoardPanel.Initial();
        _leaderBoardButton.onClick.AddListener(OnLeaderBoardButtonClick);
    }

    private void OnLeaderBoardButtonClick()
    {
        _leaderBoardPanel.Enable();
    }

    private void OnPlayerProfilePanelButtonClick()
    {
        _playerProfileDataPanel.Enable();
        _playerProfileDataPanel.Closed += OnPlayerProfileDataPanelClosed;
    }

    private void OnPlayerProfileDataPanelClosed()
    {
        _playerProfileDataPanel.Closed -= OnPlayerProfileDataPanelClosed;
        
    }

    private void OnSettingsPanelButtonClick()
    {
        _settingsPanel.Enable();
       
    }

    private void OnLevelCompleted()
    {
        _levelHandler.LevelLost -= OnLevelCompleted;
        _levelHandler.LevelWon -= OnLevelCompleted;
        
        _progressSlider.SetActive(false);
        _balanceSlider.SetActive(false);
    }
    
    public void SetPoints(int points) => 
        _pointsText.SetText(points.ToString());

    private void SetLevelName(int levelNumber) => 
        _levelText.SetText($"Уровень {levelNumber}");

}
