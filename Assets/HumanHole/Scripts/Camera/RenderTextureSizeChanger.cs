using HumanHole.Scripts.Infrastructure;
using HumanHole.Scripts.Infrastructure.Services;
using UnityEngine;

namespace HumanHole.Scripts.Camera
{
    public class RenderTextureSizeChanger : MonoBehaviour
    {
        [SerializeField] private UnityEngine.Camera _camera;
        [SerializeField] private Material _material;

        private void Awake()
        {
            RenderTexture  texture = Game.Instance.AllServices.Single<IRenderTextureService>().RenderTexture;
            _camera.targetTexture = texture;
            _material.mainTexture = texture;
        }
    }
}
