using UnityEngine;

namespace HumanHole.Scripts.Infrastructure.Services
{
    public class RenderTextureService : IRenderTextureService
    {
        public RenderTexture RenderTexture { get; }

        public RenderTextureService() => 
            RenderTexture = new RenderTexture(Screen.width, Screen.height, -1);
    }
}
