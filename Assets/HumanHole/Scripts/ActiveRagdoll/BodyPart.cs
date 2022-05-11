using UnityEngine;

namespace HumanHole.Scripts.ActiveRagdoll
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Collider))]
    public class BodyPart : MonoBehaviour
    {
        public Rigidbody Rigidbody { get; private set; }
        private Collider _collider;

        public float Mass => Rigidbody.mass;
        public Vector3 Center => _collider.bounds.center;

        protected virtual void Awake()
        {
            Rigidbody = GetComponent<Rigidbody>();
            _collider = GetComponent<Collider>();
        }
    }
}
