using UnityEngine;

namespace HumanHole.Scripts.Infrastructure.Services
{
    public interface IRenderTextureService : IService
    {
        RenderTexture RenderTexture { get; }
    }
}
