using UnityEngine.Networking;

namespace HumanHole.Scripts.Infrastructure.Services.Download
{
    public static class UnityWebRequestExtension
    {
        public static UnityWebRequestAwaiter GetAwaiter(this UnityWebRequestAsyncOperation asyncOp)
        {
            return new UnityWebRequestAwaiter(asyncOp);
        }
    }
}