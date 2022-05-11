using System;
using System.Threading.Tasks;
using HumanHole.Scripts.Infrastructure.Services.AssetsManagement;
using UnityEngine;
using Object = UnityEngine.Object;

namespace HumanHole.Scripts.Infrastructure.Services.Factory
{
    public class FactoryService: IFactoryService
    {
        private readonly IAssetProvider _assetProvider;

        public FactoryService(IAssetProvider assetProvider) => 
            _assetProvider = assetProvider;

        public async Task<T> CreateAsync<T>(string path) where T: Component
        {
            T result = await _assetProvider.Load<T>(path);
            return Create(result, Vector3.zero, Quaternion.identity);
        }

        public async Task<T> CreateAsync<T>(string path, Vector3 position, Quaternion rotation) where T: Component
        {
            T result = await _assetProvider.Load<T>(path);
            return Create(result, position, rotation);
        }

        public T Create<T>(T @object, Vector3 position, Quaternion rotation) where T: Component => 
            Object.Instantiate(@object, position, rotation);

        public T Create<T>(T @object, Transform parent) where T : Component =>
            Object.Instantiate(@object, parent);
    }
}
