using System;
using System.Collections.Generic;
using UnityEngine;

namespace HumanHole.Scripts.UI.Panels
{
    public class LevelPanelsStateMachine : MonoBehaviour
    {
        [SerializeField] private StartLevelPanel _startLevelPanel;
        [SerializeField] private LostLevelPanel _lostLevelPanel;
        [SerializeField] private WonLevelPanel _wonLevelPanel;
        [SerializeField] private LeaderBoardPanel _leaderBoardPanel;
        [SerializeField] private ShopPanel _shopPanel;

        public ILevelPanel CurrentPanel { get; private set; }
        
        private Dictionary<Type, ILevelPanel> _panels;

        public void Initial()
        {
            _panels = new Dictionary<Type, ILevelPanel>
            {
                [typeof(StartLevelPanel)] = _startLevelPanel,
                [typeof(LostLevelPanel)] = _lostLevelPanel,
                [typeof(WonLevelPanel)] = _wonLevelPanel,
                [typeof(LeaderBoardPanel)] = _leaderBoardPanel,
                [typeof(ShopPanel)] = _shopPanel
            };
        }
    
        public void SetPanel<TPanel>() where TPanel : class, ILevelPanel
        {
            CurrentPanel?.OnDisabled();
            CurrentPanel = GetPanel<TPanel>();
            CurrentPanel.OnEnabled();
        }

        public TPanel GetPanel<TPanel>() where TPanel : class, ILevelPanel => 
            _panels[typeof(TPanel)] as TPanel;
    }
}
