using UnityEngine;

namespace HumanHole.Scripts.Camera
{
    public class Cameras : MonoBehaviour
    {
        [SerializeField] private UnityEngine.Camera mainCamera;

        public UnityEngine.Camera MainCamera => mainCamera;
    }
}
