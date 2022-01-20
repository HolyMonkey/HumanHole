using System;
using YandexGames;

public interface IAuthorizationService: IService
{
    Action Authorized { get;  set; }
    Action NotAuthorized { get;  set; }
    Action GetPersonalProfileDataPermission { get;  set; }
    Action NotGetPersonalProfileDataPermission { get;  set; }
    bool HasPersonalProfileDataPermission { get;  set; }
    bool IsAuthorized { get;  set; }
}
