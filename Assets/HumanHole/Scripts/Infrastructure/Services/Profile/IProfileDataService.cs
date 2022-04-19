﻿using System;
using Agava.YandexGames;

namespace HumanHole.Scripts.Infrastructure.Services.Profile
{
    public interface IProfileDataService: IService
    {
        Action<PlayerAccountProfileDataResponse> GetProfileDataSuccess { get; set; }
        Action<string> GetProfileDataError { get; set; }
        Action GetPersonalProfileDataPermissionSuccess { get;}
        Action<string> GetPersonalProfileDataPermissionError { get; }
        bool IsPersonalProfileDataPermissionSuccess { get;}
        PlayerAccountProfileDataResponse PlayerAccountProfileDataResponse { get; set; }
        void GetProfileData();
        void RequestPersonalProfileDataPermission();
        void Initialize();
    }
}