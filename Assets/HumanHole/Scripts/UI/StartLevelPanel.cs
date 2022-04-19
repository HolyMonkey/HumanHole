using System;
using HumanHole.Scripts.Handlers;
using UnityEngine;
using UnityEngine.EventSystems;

namespace HumanHole.Scripts.UI
{
    public class StartLevelPanel : MonoBehaviour, ILevelPanel
    {
        private bool _isClicked;
        private TapHandHandler _tapHandHandler;

        public event Action Clicked;

        public void Initial(TapHandHandler tapHandHandler) => 
            _tapHandHandler = tapHandHandler;

        public void Enable()
        {
            gameObject.SetActive(true);
            _tapHandHandler.Enable();
        }
    

        public void Disable()
        {
            gameObject.SetActive(false);
            _tapHandHandler.Disable();
        }
    
        public void OnPointerClick(PointerEventData eventData)
        {
            if (!_isClicked)
            {
                _isClicked = true;
                Clicked?.Invoke();
                Disable();
            }
        }
    
    }
}
