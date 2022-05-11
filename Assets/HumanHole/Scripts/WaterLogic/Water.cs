using UnityEngine;

namespace HumanHole.Scripts.WaterLogic
{
    public class Water : MonoBehaviour
    {
        [SerializeField] private MeshRenderer _meshRenderer;
        
        public void Initial(Material material)
        {
            _meshRenderer.sharedMaterial = material;
        }
    }
}
