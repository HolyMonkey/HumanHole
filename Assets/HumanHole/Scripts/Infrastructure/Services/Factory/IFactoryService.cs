using System.Threading.Tasks;
using UnityEngine;

namespace HumanHole.Scripts.Infrastructure.Services.Factory
{
    public interface IFactoryService: IService
    {
        Task<T> CreateAsync<T>(string path) where T: Component;
        T Create<T>(T @object, Vector3 position, Quaternion rotation) where T: Component;
        T Create<T>(T @object) where T: Component;
        T Create<T>(T @object, Transform parent) where T: Component;
        Task<T> CreateAsync<T>(string path, Vector3 position, Quaternion rotation) where T: Component;
    }
}
