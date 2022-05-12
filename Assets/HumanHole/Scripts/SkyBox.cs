using HumanHole.Scripts.Level;
using UnityEngine;

namespace HumanHole.Scripts
{
    public class SkyBox
    {
        public void Initial(LevelStaticData levelStaticData)
        {
            RenderSettings.skybox = levelStaticData.SkyBoxMaterial;
            //DynamicGI.UpdateEnvironment();
        }
    }
}
