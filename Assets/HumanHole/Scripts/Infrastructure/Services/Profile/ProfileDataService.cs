using System;
using Agava.YandexGames;

namespace CodeBase.Infrastructure.Services.Profile
{
    public class ProfileDataService : IProfileDataService
    {
        private IAuthorizationService _authorizationService;
        public PlayerAccountProfileDataResponse PlayerAccountProfileDataResponse { get; set; }

        public Action GetPersonalProfileDataPermissionSuccess { get; set; }
        public Action<string> GetPersonalProfileDataPermissionError { get; set; }
        public Action<PlayerAccountProfileDataResponse> GetProfileDataSuccess { get; set; }
        public Action<string> GetProfileDataError { get; set; }

        public bool IsPersonalProfileDataPermissionSuccess { get; set; }

        public ProfileDataService(IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }

        public void Initialize()
        {
#if !UNITY_WEBGL || UNITY_EDITOR
           return;
#endif
            if (_authorizationService.IsAuthorized)
            {
                RequestPersonalProfileDataPermission();
            }
            else
            {
                _authorizationService.Authorized += OnAuthorized;
            }
        }

        public void RequestPersonalProfileDataPermission()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            PlayerAccount.RequestPersonalProfileDataPermission(OnGetPersonalProfileDataPermissionSuccess, OnGetPersonalProfileDataPermissionError);
#endif
        }

        private void OnGetPersonalProfileDataPermissionError(string message)
        {
            GetPersonalProfileDataPermissionError?.Invoke(message);
        }

        private void OnGetPersonalProfileDataPermissionSuccess()
        {
            IsPersonalProfileDataPermissionSuccess = true;
            GetPersonalProfileDataPermissionSuccess?.Invoke();
        }

        public void GetProfileData()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            if (IsPersonalProfileDataPermissionSuccess)
            {
                PlayerAccount.GetProfileData(OnGetProfileDataSuccess, OnGetProfileDataError);
            }
#endif
        }

        private void OnGetProfileDataError(string message)
        {
            GetProfileDataError?.Invoke(message);
        }

        private void OnGetProfileDataSuccess(PlayerAccountProfileDataResponse response)
        {
            PlayerAccountProfileDataResponse = response;
            GetProfileDataSuccess?.Invoke(response);
        }

        private void OnAuthorized()
        {
            _authorizationService.Authorized -= OnAuthorized;
            RequestPersonalProfileDataPermission();
        }
    }
}