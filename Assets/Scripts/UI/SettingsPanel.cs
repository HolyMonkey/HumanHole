using System;
using UnityEngine;
using UnityEngine.UI;

public class SettingsPanel: MonoBehaviour
{
    [SerializeField] private Button _getProfileDataButton;
    [SerializeField] private Button _requestPersonalProfileDataPermissionButton;
    [SerializeField] private Button _closeButton;
    
    public event Action Opened;
    public event Action Closed;

    public void Initial()
    {
        _closeButton.onClick.AddListener(Disable); 
    }
    
    public void Enable()
    {
        gameObject.SetActive(true);
        Opened?.Invoke();
    }
    

    private void Disable()
    {
        gameObject.SetActive(false);
        Closed?.Invoke();
    }
}
