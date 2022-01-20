using CodeBase.Infrastructure.Services.Profile;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YandexGames;

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
    
    public void Initial()
    {
        _profileDataService = Game.Instance.AllServices.Single<IProfileDataService>();
    }

    private void OnEnable()
    {
        _profileDataService.GetProfileDataSuccess += OnGetProfileDataSuccess;
        _profileDataService.GetProfileDataError += OnGetProfileDataError;
        _profileDataService.GetProfileData();
    }

    private void OnGetProfileDataError(string error)
    {
        _errorMessage.text = error;
    }

    private void OnGetProfileDataSuccess(PlayerAccountProfileDataResponse result)
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

    public void Enable()
    {
        gameObject.SetActive(true);
        _closeButton.onClick.AddListener(Disable); 
    }

    public void Disable()
    {
        _closeButton.onClick.RemoveListener(Disable); 
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        _profileDataService.GetProfileDataSuccess -= OnGetProfileDataSuccess;
        _profileDataService.GetProfileDataError -= OnGetProfileDataError;
    }
}
