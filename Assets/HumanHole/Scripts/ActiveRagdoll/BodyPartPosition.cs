using UnityEngine;

namespace HumanHole.Scripts.ActiveRagdoll
{
    public class BodyPartPosition : MonoBehaviour
    {
        [SerializeField] private Vector3 _offset;
        
        private Transform _originalTransform;

        public void SetTransform(Transform transform) => 
            _originalTransform = transform;

        private void LateUpdate() => 
            transform.position = _originalTransform.transform.position + _offset;
    }
}
