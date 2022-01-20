using System;
using System.Collections;
using CodeBase.Infrastructure.Services.Profile;
using UnityEngine;
using YandexGames;

public class AuthorizationService : IAuthorizationService
{
    private WaitForSecondsRealtime _waitForSecondsRealtime = new WaitForSecondsRealtime(0.25f);
    private IProfileDataService _profileDataService;

    public bool IsAuthorized { get;  set; }
    public bool HasPersonalProfileDataPermission { get; set; }
    public Action Authorized { get; set; }
    public  Action NotAuthorized { get; set; }
    public  Action GetPersonalProfileDataPermission { get; set; }
    public  Action NotGetPersonalProfileDataPermission { get; set; }

    public AuthorizationService(ICoroutineRunner coroutineRunner, IProfileDataService profileDataService)
    {
        _profileDataService = profileDataService;
        coroutineRunner.StartCoroutine(Initialize());
    }
    
    private IEnumerator Initialize()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        yield break;
#endif

        // Always wait for it if invoking something immediately in the first scene.
        yield return YandexGamesSdk.WaitForInitialization();
        
        _profileDataService.GetProfileData();
        
        while (true)
        {
            if (PlayerAccount.IsAuthorized)
            {
                if (!IsAuthorized)
                {
                    IsAuthorized = true;
                    Authorized?.Invoke();  
                }
                
                if (PlayerAccount.HasPersonalProfileDataPermission)
                {
                    if (!HasPersonalProfileDataPermission)
                    {
                        HasPersonalProfileDataPermission = true;
                        GetPersonalProfileDataPermission?.Invoke();
                    }
                }
                else
                {
                    if (HasPersonalProfileDataPermission)
                    {
                        HasPersonalProfileDataPermission = false;
                        NotGetPersonalProfileDataPermission?.Invoke();
                    }
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
