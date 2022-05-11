using System;
using HumanHole.Scripts.Data;
using HumanHole.Scripts.Handlers;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace HumanHole.Scripts.UI.Panels
{
    public class StartLevelPanel : MonoBehaviour, ILevelPanel, IPointerClickHandler
    {
        [SerializeField] private TextMeshProUGUI _levelText;
        
        public event Action Opened;
        public event Action Closed;
        public event Action Clicked;

        private TapHandHandler _tapHandHandler;

        public void Initial(TapHandHandler tapHandHandler, Progress progress)
        {
            _tapHandHandler = tapHandHandler;
            SetLevelName(progress.LevelsProgress.LevelNumber);
        }

        public void OnEnabled()
        {
            gameObject.SetActive(true);
            _tapHandHandler.Enable();
            Opened?.Invoke();
        }

        public void OnDisabled()
        {
            gameObject.SetActive(false);
            _tapHandHandler.Disable();
            Closed?.Invoke();
        }

        public void OnClicked()
        {
            OnDisabled();
            Clicked?.Invoke();
        }

        public void OnPointerClick(PointerEventData eventData) => 
            OnClicked();
        
        public void SetLevelName(int levelNumber) => 
            _levelText.SetText($"УРОВЕНЬ {levelNumber}");
    }
}
