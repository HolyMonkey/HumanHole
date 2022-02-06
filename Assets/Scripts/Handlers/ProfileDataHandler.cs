using CodeBase.Infrastructure.Services.Profile;
using UnityEngine;
using UnityEngine.UI;
using YandexGames;

public class ProfileDataHandler : MonoBehaviour
{
    private IProfileDataService _profileDataService;
    
    [SerializeField] private PlayerProfileDataPanel _playerProfileDataPanel;
    [SerializeField] private Image _personalProfileDataPermission;

    public void Initial(IProfileDataService profileDataService)
    {
        _profileDataService = profileDataService;
    }

    public void Enable()
    {
        gameObject.SetActive(true);
    }

    private void OnEnable()
    {
        if (_profileDataService.IsPersonalProfileDataPermissionSuccess)
        {
            OnGetProfileDataSuccess(_profileDataService.PlayerAccountProfileDataResponse);
        }
        else
        {
            SetImageColor(_personalProfileDataPermission,Color.red);
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
    
    private void OnGetProfileDataError(string message)
    {
        _playerProfileDataPanel.SetErrorMessage(message);
        SetImageColor(_personalProfileDataPermission,Color.red);
    }

    private void OnGetProfileDataSuccess(PlayerAccountProfileDataResponse result)
    {
        _playerProfileDataPanel.SetProfileData(result);
        SetImageColor(_personalProfileDataPermission,Color.green);
    }
    
    private void SetImageColor(Image image, Color color)
    {
        image.color = color;
    }
}
