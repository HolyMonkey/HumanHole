using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace HumanHole.Scripts.UI
{
    public class ClickZone : MonoBehaviour, IPointerClickHandler
    {
        public event Action Clicked;
    
        public void OnPointerClick(PointerEventData eventData) => 
            Clicked?.Invoke();

        public void Enable() => 
            gameObject.SetActive(true);
    
        public void Disable() => 
            gameObject.SetActive(false);
    }
}
