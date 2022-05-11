using HumanHole.Scripts.ActiveRagdoll;
using HumanHole.Scripts.Data;
using HumanHole.Scripts.Handlers;
using HumanHole.Scripts.Infrastructure.Services.Authorization;
using HumanHole.Scripts.Infrastructure.Services.Download;
using HumanHole.Scripts.Infrastructure.Services.Factory;
using HumanHole.Scripts.Infrastructure.Services.LeaderBoard;
using HumanHole.Scripts.Infrastructure.Services.SaveLoad;
using HumanHole.Scripts.Infrastructure.States;
using HumanHole.Scripts.Shop;
using HumanHole.Scripts.UI.Panels;
using HumanHole.Scripts.Wall;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HumanHole.Scripts.UI
{
    [RequireComponent(typeof(LevelPanelsStateMachine))]
    public class LevelUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _goldText;
        [SerializeField] private BalanceSlider _balanceSlider;
        [SerializeField] private ProgressSlider _progressSlider;
        [SerializeField] private Button _shopButton;
        [SerializeField] private Button _leaderBoardButton;
        [SerializeField] private LeaderBoardPanel _leaderBoardPanel;
        [SerializeField] private StartLevelPanel _startLevelPanel;
        [SerializeField] private ShopPanel _shopPanel;
        [SerializeField] private LevelsDropdown _levelsDropdown;
        [SerializeField] private ClickZone _clickZone;
        [SerializeField] private LevelPanelsStateMachine _levelPanelsStateMachine;

        public LevelPanelsStateMachine LevelPanelsStateMachine => _levelPanelsStateMachine;

        public void Initial(Progress progress, TapHandHandler tapHandHandler, Person person, WallSpawner wallSpawner, ISaveLoadService saveLoadService, GameStateMachine gameStateMachine,
            ILeaderBoardService leaderBoardService, IDownloadService downloadService, IAuthorizationService authorizationService, IFactoryService factoryService)
        {
            _levelPanelsStateMachine = GetComponent<LevelPanelsStateMachine>();
            
            InitialPanels(tapHandHandler, progress, saveLoadService, leaderBoardService, downloadService, authorizationService, factoryService);
            InitialSliders(wallSpawner, person);
            _levelsDropdown.Initial(progress,saveLoadService, gameStateMachine);
            
            SetGold(progress.GoldProgress.Count);
        }

        private void InitialSliders(WallSpawner wallSpawner, Person person)
        {
            _progressSlider.Initial(wallSpawner, person);
            _balanceSlider.Initial(person);
        }

        private void InitialPanels(TapHandHandler tapHandHandler, Progress progress, ISaveLoadService saveLoadService, ILeaderBoardService leaderBoardService,
            IDownloadService downloadService, IAuthorizationService authorizationService,
            IFactoryService factoryService)
        {
            _levelPanelsStateMachine.Initial();
            _leaderBoardPanel.Initial(_clickZone, leaderBoardService, downloadService, authorizationService);
            _startLevelPanel.Initial(tapHandHandler, progress);
            _shopPanel.Initial(progress, saveLoadService, _clickZone, factoryService);
        }

        public void OnEnabled()
        {
            _shopButton.onClick.AddListener(OnShopButtonClick);
            _leaderBoardButton.onClick.AddListener(OnLeaderBoardButtonClick);
            _startLevelPanel.Clicked += OnStartLevelPanelClicked;
            _shopPanel.Closed += OnShopPanelClicked;
        }
        
        public void OnStarted()
        {
            _leaderBoardPanel.OnStarted();
            _shopPanel.OnStarted();
        }

        private void OnShopPanelClicked() => 
            _levelPanelsStateMachine.SetPanel<StartLevelPanel>();

        private void OnStartLevelPanelClicked()
        {
            _startLevelPanel.Clicked -= OnStartLevelPanelClicked;
            EnableSliders();
        }

        public void OnDisabled()
        {
            _shopButton.onClick.RemoveListener(OnShopButtonClick);
            _leaderBoardButton.onClick.RemoveListener(OnLeaderBoardButtonClick);
        }

        private void OnShopButtonClick() => 
            _levelPanelsStateMachine.SetPanel<ShopPanel>();

        private void EnableSliders()
        {
            _balanceSlider.Enable();
            _progressSlider.Enable();
        }

        private void OnLeaderBoardButtonClick() => 
            _leaderBoardPanel.OnEnabled();

        private void DisableSliders()
        {
            _progressSlider.Disable();
            _balanceSlider.Disable();
        }

        public void SetGold(int count) => 
            _goldText.SetText(count.ToString());

        public void DisableUI() => 
            DisableSliders();
    }
}
