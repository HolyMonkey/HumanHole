using System;
using Agava.YandexGames;
using CodeBase.Infrastructure.Services.Profile;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerProfileDataPanel : MonoBehaviour
{
    private IProfileDataService _profileDataService;

    [SerializeField] private TextMeshProUGUI _userName;
    [SerializeField] private TextMeshProUGUI _uniqueId;
    [SerializeField] private TextMeshProUGUI _language;
    [SerializeField] private TextMeshProUGUI _scopePermissionPublicName;
    [SerializeField] private TextMeshProUGUI _scopePermissionAvatar;
    [SerializeField] private TextMeshProUGUI _errorMessage;
    [SerializeField] private Button _closeButton; 
    
    public event Action Opened;
    public event Action Closed;

    public void Enable()
    {
        gameObject.SetActive(true);
        _closeButton.onClick.AddListener(Disable); 
        Opened?.Invoke();
    }

    public void Disable()
    {
        if (gameObject.activeSelf)
        {
            _closeButton.onClick.RemoveListener(Disable);
            gameObject.SetActive(false);
            Closed?.Invoke();
        }
    }

    public void SetErrorMessage(string message)
    {
        _errorMessage.text = message;
    }

    public void SetProfileData(PlayerAccountProfileDataResponse result)
    {
        string name = result.publicName;
        if (string.IsNullOrEmpty(name)) 
            name = "Anonymous";
        
        _userName.text = name;
        _uniqueId.text = result.uniqueID;
        _language.text = result.lang;
        _scopePermissionPublicName.text = result.scopePermissions.public_name;
        _scopePermissionAvatar.text = result.scopePermissions.avatar;
    }
}
