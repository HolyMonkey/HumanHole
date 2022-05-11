using UnityEngine;

namespace HumanHole.Scripts
{
    public class Platform : MonoBehaviour
    {
        [SerializeField] private MeshRenderer _platFormMeshRenderer;
        
        public void Initial(Color color) => 
            _platFormMeshRenderer.sharedMaterial.color = color;
    }
}
