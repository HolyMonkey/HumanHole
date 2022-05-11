using System.Threading.Tasks;
using UnityEngine;

namespace HumanHole.Scripts.Infrastructure.Services.AssetsManagement
{
    public interface IAssetProvider : IService
    {
        Task<T> Load<T>(string path) where T : Component;
    }
}
