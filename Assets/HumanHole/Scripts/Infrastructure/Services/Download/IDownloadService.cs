using System.Threading.Tasks;
using UnityEngine;

public interface IDownloadService: IService
{
    Task<Texture2D> DownloadPreview(string path);
}
