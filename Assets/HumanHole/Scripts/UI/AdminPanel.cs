using HumanHole.Scripts.Data;
using HumanHole.Scripts.Handlers;
using HumanHole.Scripts.Infrastructure.Services.SaveLoad;
using HumanHole.Scripts.Infrastructure.States;
using HumanHole.Scripts.LevelLogic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HumanHole.Scripts.UI
{
    public class AdminPanel : MonoBehaviour
    {
        [SerializeField] private Button _clearDataButton;
        [SerializeField] private Button _setGoldButton;
        [SerializeField] private TMP_InputField _goldInputField;
        [SerializeField] private TMP_Dropdown _dropdown;
        
        private Progress _progress;
        private ISaveLoadService _saveLoadService;
        private GameStateMachine _gameStateMachine;
        private LevelUI _levelUI;

        public void Initial(Progress progress, ISaveLoadService saveLoadService, GameStateMachine gameStateMachine, LevelsStaticData levelsStaticData, LevelUI levelUI)
        {
            _levelUI = levelUI;
            _saveLoadService = saveLoadService;
            _gameStateMachine = gameStateMachine;
            _progress = progress;
            _dropdown.options.Clear();

            int levelsCount = levelsStaticData.LevelsStaticDataList.Count;

            for (int i = 0; i < levelsCount; i++)
            {
                string levelName = $"Level {i + 1}";
                _dropdown.options.Add(new TMP_Dropdown.OptionData
                {
                    text = levelName
                });
            }
            
            int currentLevelIndex = _progress.LevelsProgress.LevelNumber - 1;
            _dropdown.value = currentLevelIndex;
            _dropdown.captionText.text = _dropdown.options[currentLevelIndex].text;
        }

        public void OnEnabled()
        {
            _dropdown.onValueChanged.AddListener(OnDropdownValueChanged);
            _setGoldButton.onClick.AddListener(OnSetGoldButtonClicked);
            _clearDataButton.onClick.AddListener(OnClearDataButtonClicked);
        }

        public void OnDisabled()
        {
            _dropdown.onValueChanged.RemoveListener(OnDropdownValueChanged);
            _setGoldButton.onClick.RemoveListener(OnSetGoldButtonClicked);
            _clearDataButton.onClick.RemoveListener(OnClearDataButtonClicked);
        }

        private void OnSetGoldButtonClicked()
        {
           int.TryParse(_goldInputField.text, out int gold);
           if (gold > -1)
           {
               _progress.GoldProgress.Set(gold);
               _saveLoadService.SaveProgress();
               _levelUI.SetGold(gold);
           }
        }

        private void OnClearDataButtonClicked()
        {
            _saveLoadService.ClearProgress();
            _saveLoadService.SaveProgress();
        }

        private void OnDropdownValueChanged(int value)
        {
            _progress.LevelsProgress.SetLevelNumber(value + 1);
            _saveLoadService.Saved += OnProgressSaved;
            _saveLoadService.SaveProgress();
        
        }

        private void OnProgressSaved()
        {
            _saveLoadService.Saved -= OnProgressSaved;
            _gameStateMachine.Enter<LoadLevelState, string, bool>(_progress.LevelsProgress.LevelName, true);
        }
    }
}
