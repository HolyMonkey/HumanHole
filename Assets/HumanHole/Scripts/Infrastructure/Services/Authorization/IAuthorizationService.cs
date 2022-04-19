using System;
using System.Collections;

namespace HumanHole.Scripts.Infrastructure.Services.Authorization
{
    public interface IAuthorizationService: IService
    {
        Action Authorized { get;  set; }
        Action NotAuthorized { get;  set; }
        bool IsAuthorized { get;  set; }
        IEnumerator Authorize();
    }
}
