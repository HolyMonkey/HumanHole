using Agava.YandexGames;
using CodeBase.Infrastructure.Services.Profile;
using UnityEngine;

public class ProfileDataHandler : MonoBehaviour
{
    private IProfileDataService _profileDataService;
    private PlayerProfileDataPanel _playerProfileDataPanel;

    public void Initial(IProfileDataService profileDataService, PlayerProfileDataPanel profileDataPanel)
    {
        _profileDataService = profileDataService;
        _playerProfileDataPanel = profileDataPanel;
    }

    public void Enable() => 
        gameObject.SetActive(true);

    private void OnEnable()
    {
        if (_profileDataService.IsPersonalProfileDataPermissionSuccess)
        {
            OnGetProfileDataSuccess(_profileDataService.PlayerAccountProfileDataResponse);
        }
        else
        {
            _profileDataService.GetProfileDataSuccess += OnGetProfileDataSuccess;
            _profileDataService.GetProfileDataError += OnGetProfileDataError;
            _playerProfileDataPanel.Opened += OnPlayerProfileDataPanelOpened;
        }
    }

    private void OnDisable()
    {
        _profileDataService.GetProfileDataSuccess -= OnGetProfileDataSuccess;
        _profileDataService.GetProfileDataError -= OnGetProfileDataError;
    }

    private void OnPlayerProfileDataPanelOpened()
    {
        _playerProfileDataPanel.Opened -= OnPlayerProfileDataPanelOpened;
        _profileDataService.GetProfileData();
    }
    
    private void OnGetProfileDataError(string message) => 
        _playerProfileDataPanel.SetErrorMessage(message);

    private void OnGetProfileDataSuccess(PlayerAccountProfileDataResponse result) => 
        _playerProfileDataPanel.SetProfileData(result);
}
