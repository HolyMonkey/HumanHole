using System;
using YandexGames;

namespace CodeBase.Infrastructure.Services.Profile
{
    public class ProfileDataService: IProfileDataService
    {
        public Action GetPersonalProfileDataPermissionSuccess { get; set; }
        public Action<string> GetPersonalProfileDataPermissionError { get; set; }
        public Action<PlayerAccountProfileDataResponse> GetProfileDataSuccess { get; set; }
        public Action<string> GetProfileDataError { get; set; }

        public void RequestPersonalProfileDataPermission()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            PlayerAccount.RequestPersonalProfileDataPermission(GetPersonalProfileDataPermissionSuccess, GetPersonalProfileDataPermissionError);
#endif
        }
        
        public void GetProfileData()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            PlayerAccount.GetProfileData(GetProfileDataSuccess, GetProfileDataError);
#endif
        }
    }
}