using UnityEngine;
public class AuthorizationHandler : MonoBehaviour
{
    private IAuthorizationService _authorizationService;

    public void Initial(IAuthorizationService authorizationService) => 
        _authorizationService = authorizationService;

    public void Enable() => 
        gameObject.SetActive(true);

    private void OnEnable()
    {
        if (!_authorizationService.IsAuthorized)
        {
            _authorizationService.Authorized += OnAuthorized;
        }
    }

    private void OnAuthorized() => 
        Debug.Log("Authorized");
}
