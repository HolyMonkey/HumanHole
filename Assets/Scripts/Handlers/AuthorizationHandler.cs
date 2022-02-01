using UnityEngine;
using UnityEngine.UI;

public class AuthorizationHandler : MonoBehaviour
{
    private IAuthorizationService _authorizationService;
    
    [SerializeField] private Image _authorizedImage;

    public void Initial(IAuthorizationService authorizationService)
    {
        _authorizationService = authorizationService;
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
