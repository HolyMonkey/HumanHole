using HumanHole.Scripts.Infrastructure;
using HumanHole.Scripts.Infrastructure.Services;
using UnityEngine;

namespace HumanHole.Scripts.Camera
{
    public class RenderTextureSizeChanger : MonoBehaviour
    {
        [SerializeField] private Material _material;
        
        private UnityEngine.Camera _camera;

        private void Awake()
        {
            RenderTexture  texture = Game.Instance.AllServices.Single<IRenderTextureService>().RenderTexture;
            _camera = GetComponent<UnityEngine.Camera>();
            _camera.targetTexture = texture;
            _material.mainTexture = texture;
        }
    }
}
