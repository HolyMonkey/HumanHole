using System;
using System.Collections;
using Agava.YandexGames;
using CodeBase.Infrastructure.Services.Profile;
using UnityEngine;

public class AuthorizationService : IAuthorizationService
{
    private WaitForSecondsRealtime _waitForSecondsRealtime = new WaitForSecondsRealtime(0.25f);
    private IProfileDataService _profileDataService;

    public bool IsAuthorized { get;  set; }
    public Action Authorized { get; set; }
    public  Action NotAuthorized { get; set; }

    public AuthorizationService(ICoroutineRunner coroutineRunner)
    {
        coroutineRunner.StartCoroutine(Initialize());
    }
    
    private IEnumerator Initialize()
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
