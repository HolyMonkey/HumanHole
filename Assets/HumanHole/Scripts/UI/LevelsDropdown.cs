using HumanHole.Scripts.Data;
using HumanHole.Scripts.Infrastructure.Services.SaveLoad;
using HumanHole.Scripts.Infrastructure.States;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HumanHole.Scripts.UI
{
    public class LevelsDropdown : MonoBehaviour
    {
        private TMP_Dropdown _dropdown;
        private Progress _progress;
        private ISaveLoadService _saveLoadService;
        private GameStateMachine _gameStateMachine;
        private string _nextLevel;

        public void Initial(Progress progress, ISaveLoadService saveLoadService, GameStateMachine gameStateMachine)
        {
            _saveLoadService = saveLoadService;
            _gameStateMachine = gameStateMachine;
            _progress = progress;
            _dropdown = GetComponent<TMP_Dropdown>();
            _dropdown.options.Clear();

            int levelsCount = SceneManager.sceneCountInBuildSettings - 1;
        
            //List<string> levels = new List<string>() {"Level 1", "Level 2","Level 3", "Level 4","Level 5", "Level 6","Level 7", "Level 8", "Level 9"};

            for (int i = 0; i < levelsCount; i++)
            {
                string levelName = $"Level {i + 1}";
                _dropdown.options.Add(new TMP_Dropdown.OptionData() {text = levelName});
            }
        
            /*foreach (var level in levels)
        {
            _dropdown.options.Add(new TMP_Dropdown.OptionData() {text = level});
        }*/

            int buildIndex = SceneManager.GetActiveScene().buildIndex;
            int currentLevelIndex = buildIndex - 1;
        
            _dropdown.value = currentLevelIndex;
            _dropdown.captionText.text = _dropdown.options[currentLevelIndex].text;
            _dropdown.onValueChanged.AddListener(OnDropdownValueChanged);
        }

        private void OnDropdownValueChanged(int value)
        {
            _nextLevel = _dropdown.options[value].text;
            _progress.SetLevelNumber(value + 1);
            _saveLoadService.Saved += OnProgressSaved;
            _saveLoadService.SaveProgress();
        
        }

        private void OnProgressSaved()
        {
            _saveLoadService.Saved -= OnProgressSaved;
            _gameStateMachine.Enter<LoadLevelState, string, bool>(_nextLevel, true);
        }
    }
}
