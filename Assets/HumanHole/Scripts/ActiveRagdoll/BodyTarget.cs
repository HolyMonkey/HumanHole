using UnityEngine;

namespace HumanHole.Scripts.ActiveRagdoll
{
    public class BodyTarget : MonoBehaviour
    {
        [SerializeField] private SpringJoint _springJoint;
        
        private Transform _anchorPoint;
        private Rigidbody _connectedBody;

        public void Initial(Person person)
        {
            _anchorPoint = person.BodyAnchorTransform;
            _connectedBody = person.SpineRigidbody;
        }
        
        public void Enable()
        {
            gameObject.SetActive(true);
            _springJoint.connectedAnchor = _anchorPoint.localPosition;
            _springJoint.connectedBody = _connectedBody;
        }

        public void SetPosition(Vector3 position) => 
            transform.position = position;
    }
}
