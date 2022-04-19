using UnityEngine;

namespace HumanHole.Scripts.ActiveRagdoll
{
    public class BodyPartRotation : MonoBehaviour
    {
        private Transform _originalTransform;

        public void SetTransform(Transform transform) => 
            _originalTransform = transform;

        private void LateUpdate() => 
            transform.rotation = Quaternion.LookRotation(_originalTransform.forward, Vector3.up);
    }
}
