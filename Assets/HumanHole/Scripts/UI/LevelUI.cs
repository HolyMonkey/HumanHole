using System;
using HumanHole.Scripts.ActiveRagdoll;
using HumanHole.Scripts.Data;
using HumanHole.Scripts.Handlers;
using HumanHole.Scripts.Infrastructure.Services.SaveLoad;
using HumanHole.Scripts.Infrastructure.States;
using HumanHole.Scripts.Spawners;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HumanHole.Scripts.UI
{
    [RequireComponent(typeof(LevelPanelsStateMachine))]
    public class LevelUI : MonoBehaviour
    {
        private LevelHandler _levelHandler;

        [SerializeField] private TextMeshProUGUI _levelText;
        [SerializeField] private TextMeshProUGUI _pointsText;
        [SerializeField] private BalanceSlider _balanceSlider;
        [SerializeField] private ProgressSlider _progressSlider;
        [SerializeField] private Button _showRewarButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _playerProfileButton;
        [SerializeField] private Button _leaderBoardButton;

        [SerializeField] private LevelPanelsStateMachine _levelPanelsStateMachine;
        [SerializeField] private SettingsPanel _settingsPanel;
        [SerializeField] private PlayerProfileDataPanel _playerProfileDataPanel;
        [SerializeField] private LeaderBoardPanel _leaderBoardPanel;
        [SerializeField] private WonLevelPanel _wonLevelPanel;
        [SerializeField] private StartLevelPanel _startLevelPanel;
        [SerializeField] private LostLevelPanel _lostLevelPanel;
        [SerializeField] private LevelsDropdown _levelsDropdown;

        public PlayerProfileDataPanel PlayerProfileDataPanel => _playerProfileDataPanel;
        public SettingsPanel SettingsPanel => _settingsPanel;
        public LeaderBoardPanel LeaderBoardPanel => _leaderBoardPanel;
        public LevelPanelsStateMachine LevelPanelsStateMachine => _levelPanelsStateMachine;
    
        public event Action ShowRewardAdButtonClick;

        public void Initial(LevelHandler levelHandler, Progress progress, TapHandHandler tapHandHandler, Person person, WallSpawner wallSpawner, ISaveLoadService saveLoadService, GameStateMachine gameStateMachine)
        {
            _levelHandler = levelHandler;
            InitialPanels(tapHandHandler);
            InitialSliders(wallSpawner, person);
            _levelsDropdown.Initial(progress,saveLoadService, gameStateMachine);

            SetLevelName(progress.LevelNumber);
            SetPoints(progress.Points);

            _showRewarButton.onClick.AddListener(()=>ShowRewardAdButtonClick?.Invoke());
            _playerProfileButton.onClick.AddListener(OnPlayerProfilePanelButtonClick);
            _settingsButton.onClick.AddListener(OnSettingsPanelButtonClick);
            _levelHandler.LevelLost += OnLevelCompleted;
            _levelHandler.LevelWon += OnLevelCompleted;

            _leaderBoardButton.onClick.AddListener(OnLeaderBoardButtonClick);

            EnableSliders();
        }

        private void EnableSliders()
        {
            _balanceSlider.Enable();
            _progressSlider.Enable();
        }

        private void InitialSliders(WallSpawner wallSpawner, Person person)
        {
            _progressSlider.Initial(wallSpawner, person);
            _balanceSlider.Initial(person);
        }

        private void InitialPanels(TapHandHandler tapHandHandler)
        {
            _levelPanelsStateMachine.Initial();
            _settingsPanel.Initial();
            _leaderBoardPanel.Initial();
            _wonLevelPanel.Initial(tapHandHandler);
            _startLevelPanel.Initial(tapHandHandler);
            _lostLevelPanel.Initialize(tapHandHandler);
        }

        private void OnLeaderBoardButtonClick() => 
            _leaderBoardPanel.Enable();

        private void OnPlayerProfilePanelButtonClick()
        {
            _playerProfileDataPanel.Enable();
            _playerProfileDataPanel.Closed += OnPlayerProfileDataPanelClosed;
        }

        private void OnPlayerProfileDataPanelClosed() => 
            _playerProfileDataPanel.Closed -= OnPlayerProfileDataPanelClosed;

        private void OnSettingsPanelButtonClick() => 
            _settingsPanel.Enable();

        private void OnLevelCompleted()
        {
            _levelHandler.LevelLost -= OnLevelCompleted;
            _levelHandler.LevelWon -= OnLevelCompleted;
        
            _progressSlider.gameObject.SetActive(false);
            _balanceSlider.gameObject.SetActive(false);
        }
    
        public void SetPoints(int points) => 
            _pointsText.SetText(points.ToString());

        private void SetLevelName(int levelNumber) => 
            _levelText.SetText($"Уровень {levelNumber}");

    }
}
