using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace HumanHole.Scripts.Infrastructure.Services.Download
{
    public class DownloadService : IDownloadService
    {
        public async Task<Texture2D> DownloadPreview(string path)
        {
            using (UnityWebRequest www = UnityWebRequestTexture.GetTexture(path))
            {
                await www.SendWebRequest();

                if (www.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogError(www.error);
                    return null;
                }

                return DownloadHandlerTexture.GetContent(www);
            }
        }
    }
}

