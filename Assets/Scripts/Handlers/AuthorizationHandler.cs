using UnityEngine;
using UnityEngine.UI;

public class AuthorizationHandler : MonoBehaviour
{
    private IAuthorizationService _authorizationService;
    
    [SerializeField] private SettingsPanel _settingsPanel;
    [SerializeField] private Image _authorizedImage;
    [SerializeField] private Image _personalProfileDataPermission;
    public void Initial()
    {
        _authorizationService = Game.Instance.AllServices.Single<IAuthorizationService>();
    }

    public void Enable()
    {
        gameObject.SetActive(true);
    }
    
    private void OnEnable()
    {
        if (_authorizationService.IsAuthorized)
        {
            SetImageColor(_authorizedImage,Color.green);
        }
        else
        {
            SetImageColor(_authorizedImage,Color.red);
            _authorizationService.Authorized += OnAuthorized;
        }

        if (_authorizationService.HasPersonalProfileDataPermission)
        {
            SetImageColor(_personalProfileDataPermission,Color.green);
        }
        else
        {
            SetImageColor(_personalProfileDataPermission,Color.red);
            _authorizationService.GetPersonalProfileDataPermission += OnGetPersonalProfileDataPermission;
        }
    }

    private void OnGetPersonalProfileDataPermission()
    {
        SetImageColor(_personalProfileDataPermission,Color.green);
    }

    private void OnAuthorized()
    {
        SetImageColor(_authorizedImage,Color.green);
    }

    private void SetImageColor(Image image, Color color)
    {
        image.color = color;
    }
}
