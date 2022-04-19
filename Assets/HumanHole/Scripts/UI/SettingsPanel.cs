using System;
using UnityEngine;
using UnityEngine.UI;

namespace HumanHole.Scripts.UI
{
    public class SettingsPanel: MonoBehaviour
    {
        [SerializeField] private Button _closeButton;
    
        public event Action Opened;
        public event Action Closed;

        public void Initial() => 
            _closeButton.onClick.AddListener(Disable);

        public void Enable()
        {
            gameObject.SetActive(true);
            Opened?.Invoke();
        }
    

        private void Disable()
        {
            if (gameObject.activeSelf)
            {
                gameObject.SetActive(false);
                Closed?.Invoke();
            }
        }
    }
}
