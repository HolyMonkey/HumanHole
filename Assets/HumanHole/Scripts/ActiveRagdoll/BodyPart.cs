using UnityEngine;

namespace HumanHole.Scripts.ActiveRagdoll
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Collider))]
    public class BodyPart : MonoBehaviour
    {
        private Rigidbody _rigidbody;
        private Collider _collider;

        public float Mass => _rigidbody.mass;
        public Vector3 Center => _collider.bounds.center;
        public Person Body
        {
            get
            {
                Transform currentTransform = transform;
                while (currentTransform.parent != null)
                {
                    currentTransform = currentTransform.parent;
                    if (currentTransform.gameObject.TryGetComponent(out Person person))
                        return person;
                }

                return null;
            }
        }

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _collider = GetComponent<Collider>();
        }
    }
}
