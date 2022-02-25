using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelsDropdown : MonoBehaviour
{
    private TMP_Dropdown _dropdown;
    private Progress _progress;
    private ISaveLoadService _saveLoadService;
    private GameStateMachine _gameStateMachine;
    
    public void Initial(Progress progress, ISaveLoadService saveLoadService, GameStateMachine gameStateMachine)
    {
        _saveLoadService = saveLoadService;
        _gameStateMachine = gameStateMachine;
        _progress = progress;
        _dropdown = GetComponent<TMP_Dropdown>();
        _dropdown.options.Clear();
        
        List<string> levels = new List<string>() {"Level 1", "Level 2","Level 3", "Level 4","Level 5", "Level 6","Level 7", "Level 8", "Level 9"};
        foreach (var level in levels)
        {
            _dropdown.options.Add(new TMP_Dropdown.OptionData() {text = level});
        }

        int buildIndex = SceneManager.GetActiveScene().buildIndex;
        int currentLevelIndex = buildIndex - 1;
        
        _dropdown.value = currentLevelIndex;
        _dropdown.captionText.text = _dropdown.options[currentLevelIndex].text;
        _dropdown.onValueChanged.AddListener(OnDropdownValueChanged);
    }

    private void OnDropdownValueChanged(int value)
    {
        string level = _dropdown.options[value].text;
        _progress.SetLevelNumber(value + 1);
        _saveLoadService.SaveProgress();
        _gameStateMachine.Enter<LoadLevelState, string, bool>(level, true);
    }
}
