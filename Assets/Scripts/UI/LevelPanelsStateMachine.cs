using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelPanelsStateMachine : MonoBehaviour
{
    private Dictionary<Type, ILevelPanel> _panels;
    
    [SerializeField] private StartLevelPanel _startLevelPanel;
    [SerializeField] private LostLevelPanel _lostLevelPanel;
    [SerializeField] private WonLevelPanel _wonLevelPanel;
    
    public ILevelPanel CurrentPanel { get; private set; }
    
    public void Initial()
    {
        _panels = new Dictionary<Type, ILevelPanel>()
        {
            [typeof(StartLevelPanel)] = _startLevelPanel,
            [typeof(LostLevelPanel)] = _lostLevelPanel,
            [typeof(WonLevelPanel)] = _wonLevelPanel
        };
    }
    
    public void SwitchPanel<TPanel>() where TPanel : class, ILevelPanel
    {
        CurrentPanel?.Disable();
        CurrentPanel = GetPanel<TPanel>();
        CurrentPanel.Enable();
    }

    private TPanel GetPanel<TPanel>() where TPanel : class, ILevelPanel => 
        _panels[typeof(TPanel)] as TPanel;
}
