using System;
using UnityEngine;

public class LevelPauseHandler : MonoBehaviour
{
    [SerializeField] private SettingsPanel _settingsPanel;

    public bool IsPaused { get; private set; }
    
    public event Action OnPause;
    public event Action OffPause;
    
    public void Initial()
    {
        _settingsPanel.Opened += OnSettingsPanelOpened;
        _settingsPanel.Closed += OnSettingsPanelClosed;
    }

    private void OnSettingsPanelClosed()
    {
        IsPaused = false;
        OffPause?.Invoke();
    }

    private void OnSettingsPanelOpened()
    {
        IsPaused = true;
        OnPause?.Invoke();
    }
}
