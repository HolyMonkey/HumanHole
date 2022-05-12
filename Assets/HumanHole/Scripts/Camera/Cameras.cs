using HumanHole.Scripts.Infrastructure.Services;
using UnityEngine;

namespace HumanHole.Scripts.Camera
{
    public class Cameras : MonoBehaviour
    {
        [SerializeField] private UnityEngine.Camera _mainCamera;
        [SerializeField] private UnityEngine.Camera _handCamera;
        [SerializeField] private Material _handMaterial;

        public UnityEngine.Camera MainCamera => _mainCamera;
        public UnityEngine.Camera HandCamera => _handCamera;

        public void Initial(IRenderTextureService renderTextureService)
        {
            RenderTexture texture = renderTextureService.RenderTexture;
            _handCamera.targetTexture = texture;
            _handMaterial.mainTexture = texture;
        }
    }
}
