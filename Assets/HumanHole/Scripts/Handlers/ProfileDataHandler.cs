using Agava.YandexGames;
using HumanHole.Scripts.Infrastructure.Services.Profile;
using HumanHole.Scripts.UI;
using UnityEngine;

namespace HumanHole.Scripts.Handlers
{
    public class ProfileDataHandler
    {
        private IProfileDataService _profileDataService;

        public void Initial(IProfileDataService profileDataService) => 
            _profileDataService = profileDataService;

        public void OnEnabled()
        {
            if (_profileDataService.IsPersonalProfileDataPermissionSuccess)
            {
                OnGetProfileDataSuccess(_profileDataService.PlayerAccountProfileDataResponse);
            }
            else
            {
                _profileDataService.GetProfileDataSuccess += OnGetProfileDataSuccess;
                _profileDataService.GetProfileDataError += OnGetProfileDataError;
            }
        }

        public void OnDisabled()
        {
            _profileDataService.GetProfileDataSuccess -= OnGetProfileDataSuccess;
            _profileDataService.GetProfileDataError -= OnGetProfileDataError;
        }

        private void OnGetProfileDataError(string message)
        {
        }

        private void OnGetProfileDataSuccess(PlayerAccountProfileDataResponse result)
        {
        }
    }
}
