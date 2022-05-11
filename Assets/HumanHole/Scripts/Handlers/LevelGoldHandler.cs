using HumanHole.Scripts.Data;
using HumanHole.Scripts.Infrastructure.Services.SaveLoad;
using HumanHole.Scripts.Level;
using HumanHole.Scripts.UI;
using HumanHole.Scripts.UI.Panels;

namespace HumanHole.Scripts.Handlers
{
    public class LevelGoldHandler
    {
        private LevelUI _levelUI;
        private LevelHandler _levelHandler;
        private Progress _progress;
        private ISaveLoadService _saveLoadService;
        private AdHandler _adHandler;
        private WonLevelPanel _wonLevelPanel;
        private LostLevelPanel _lostLevelPanel;
        private LevelStaticData _levelStaticData;

        private int _rewardedGold;
        private int _progressGold;
        private int _levelGold;
        private bool _wonLevel;

        public void Initial(LevelUI levelUI, LevelHandler levelHandler, Progress progress,
            ISaveLoadService saveLoadService, AdHandler adHandler, LevelPanelsStateMachine levelPanelsStateMachine,
            LevelStaticData levelStaticData)
        {
            _levelStaticData = levelStaticData;
            _wonLevelPanel = levelPanelsStateMachine.GetPanel<WonLevelPanel>();
            _lostLevelPanel = levelPanelsStateMachine.GetPanel<LostLevelPanel>();
            _adHandler = adHandler;
            _saveLoadService = saveLoadService;
            _progress = progress;
            _levelHandler = levelHandler;
            _levelUI = levelUI;
        }

        public void OnEnabled()
        {
            _levelHandler.LevelWon += OnLevelWon;
            _levelHandler.LevelLost += OnLevelLost;
            _adHandler.RewardAdShowed += OnRewardedAdShowed;
            _progress.GoldProgress.Changed += OnGoldChanged;
        }

        public void OnStarted() =>
            OnGoldChanged();

        private void OnGoldChanged()
        {
            _progressGold = _progress.GoldProgress.Count;
            SetLevelAllGold(_progressGold);
            if(_wonLevel)
                SetLevelWonGold(_levelGold + _rewardedGold);
            else
                SetLevelLoseGold(_levelGold + _rewardedGold);
        }

        public void OnDisabled()
        {
            _levelHandler.LevelWon -= OnLevelWon;
            _adHandler.RewardAdShowed -= OnRewardedAdShowed;
            _progress.GoldProgress.Changed -= OnGoldChanged;
        }

        private void OnRewardedAdShowed()
        {
            if (_wonLevel)
            {
                _rewardedGold = _levelStaticData.LevelWonGold;
                _wonLevelPanel.DisableRewardButton();
            }
            else
            {
                _rewardedGold = _levelStaticData.LevelLoseGold;
                _lostLevelPanel.DisableRewardButton();
            }
            
            SaveGold(_rewardedGold);
        }

        private void SetLevelAllGold(int gold) =>
            _levelUI.SetGold(gold);

        private void SetLevelWonGold(int gold) => 
            _wonLevelPanel.SetGold(gold);

        private void SetLevelLoseGold(int gold) => 
            _lostLevelPanel.SetGold(gold);

        private void OnLevelWon()
        {
            _wonLevel = true;
            SetLevelGold(_levelStaticData.LevelWonGold);
            SaveGold(_levelStaticData.LevelWonGold);
        }

        private void OnLevelLost()
        {
            SetLevelGold(_levelStaticData.LevelLoseGold);
            SaveGold(_levelStaticData.LevelLoseGold);
        }

        private void SetLevelGold(int gold) =>
            _levelGold = gold;

        private void SaveGold(int gold)
        {
            _progress.GoldProgress.Add(gold);
            _saveLoadService.SaveProgress();
        }
    }
}