using UnityEngine;

public class RenderTextureSizeChanger : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Material _material;

    private void Awake()
    {
        RenderTexture  texture = Game.Instance.AllServices.Single<IRenderTextureService>().RenderTexture;
        _camera.targetTexture = texture;
        _material.mainTexture = texture;
    }
}
