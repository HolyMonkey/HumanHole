using System;
using System.Collections;
using Agava.YandexGames;
using HumanHole.Scripts.Infrastructure.Services.Profile;
using UnityEngine;

namespace HumanHole.Scripts.Infrastructure.Services.Authorization
{
    public class AuthorizationService : IAuthorizationService
    {
        private WaitForSecondsRealtime _waitForSecondsRealtime = new WaitForSecondsRealtime(0.25f);
        private IProfileDataService _profileDataService;

        public bool IsAuthorized { get;  set; }
        public Action Authorized { get; set; }
        public  Action NotAuthorized { get; set; }
    
        public IEnumerator Authorize()
        {
#if !UNITY_WEBGL || UNITY_EDITOR
            yield break;
#endif
            while (true)
            {
                if (PlayerAccount.IsAuthorized)
                {
                    if (!IsAuthorized)
                    {
                        IsAuthorized = true;
                        Authorized?.Invoke();  
                    }
                }
                else
                {
                    if (IsAuthorized)
                    {
                        IsAuthorized = false;
                        NotAuthorized?.Invoke();
                    }
                }

                yield return _waitForSecondsRealtime;
            }
        }
    }
}