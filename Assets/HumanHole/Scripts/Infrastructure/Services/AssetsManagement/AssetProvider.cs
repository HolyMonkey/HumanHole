using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Object = System.Object;

namespace HumanHole.Scripts.Infrastructure.Services.AssetsManagement
{
    public class AssetProvider : IAssetProvider
    {
        private Dictionary<Type, Object> _components = new Dictionary<Type, Object>();
        public async Task<T> Load<T>(string path) where T : Component
        {
            if (_components.TryGetValue(typeof(T), out Object value))
                return (T) value;

            ResourceRequest resourceRequest = Resources.LoadAsync<T>(path);
            while (!resourceRequest.isDone) 
                await Task.Yield();
            
            _components.Add(typeof(T), resourceRequest.asset);
            return (T) resourceRequest.asset;
        }
        
    }
}
