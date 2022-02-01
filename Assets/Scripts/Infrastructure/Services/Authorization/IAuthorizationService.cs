using System;
using YandexGames;

public interface IAuthorizationService: IService
{
    Action Authorized { get;  set; }
    Action NotAuthorized { get;  set; }
    bool IsAuthorized { get;  set; }
}
