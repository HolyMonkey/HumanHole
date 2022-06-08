using System;
using System.Collections.Generic;
using HumanHole.Scripts.Data;
using HumanHole.Scripts.Handlers;
using HumanHole.Scripts.Infrastructure.Services.Localization;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.SmartFormat.PersistentVariables;

namespace HumanHole.Scripts.UI.Panels
{
    public class StartLevelPanel : MonoBehaviour, ILevelPanel, IPointerClickHandler
    {
        private const string LevelNumberLocalizationKey = "LevelNumber";
        
        [SerializeField] private LocalizeStringEvent _localizeStringEvent;
        
        public event Action Opened;
        public event Action Closed;
        public event Action Clicked;

        private TapHandHandler _tapHandHandler;

        public void Initial(TapHandHandler tapHandHandler, Progress progress, ILocalizationService localizationService)
        {
            _tapHandHandler = tapHandHandler;
            localizationService.AddLocalVariable(_localizeStringEvent.StringReference,LevelNumberLocalizationKey, progress.LevelsProgress.LevelNumber);
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
    }
}
