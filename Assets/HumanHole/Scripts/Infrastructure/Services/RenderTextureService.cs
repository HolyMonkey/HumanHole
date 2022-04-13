using UnityEngine;

public class RenderTextureService : IRenderTextureService
{
    public RenderTexture RenderTexture { get; }

    public RenderTextureService()
    {
        RenderTexture = new RenderTexture(Screen.width, Screen.height, -1);
       // RenderTexture.Create();
    }
}
