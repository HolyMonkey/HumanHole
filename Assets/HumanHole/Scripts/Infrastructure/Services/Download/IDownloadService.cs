using System.Threading.Tasks;
using UnityEngine;

namespace HumanHole.Scripts.Infrastructure.Services.Download
{
    public interface IDownloadService: IService
    {
        Task<Texture2D> DownloadPreview(string path);
    }
}
