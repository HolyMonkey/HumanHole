using UnityEngine;

namespace HumanHole.Scripts.ActiveRagdoll
{
    public class HandControl : MonoBehaviour
    {
        [SerializeField] private ConfigurableJoint _target;
        [SerializeField] private Transform _shoulder;

        private float _distance;

        private void Awake()
        {
            _distance = _target.connectedAnchor.magnitude;
        }

        private void Update()
        {
            float distance = Vector3.Distance(transform.position, _shoulder.position);
            if (distance > 0.1f)
            {
                _target.transform.position = _shoulder.position + (transform.position - _shoulder.position).normalized * Mathf.Max(_distance, distance);
            }
        }
    }
}
