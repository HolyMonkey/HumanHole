using HumanHole.Scripts.ActiveRagdoll;
using UnityEngine;

namespace HumanHole.Scripts.Player
{
    public class Сharacter : MonoBehaviour
    {
        [SerializeField] private int _id;
        [SerializeField] private int _price;
        [SerializeField] private BodyPartRotation _spineRotation;
        [SerializeField] private BodyPartPosition _leftHandAnchorPosition;
        [SerializeField] private BodyPartPosition _rightHandAnchorPosition;
        [SerializeField] private BodyPartPosition _rightFootAnchorPosition;
        [SerializeField] private BodyPartPosition _leftFootAnchorPosition;
        [SerializeField] private BodyPartPosition _bodyAnchorPosition;

        public int Id => _id;
        
        public void SetPerson(Person person)
        {
            SetTransforms(person.SpineTransform,person.LeftArmTransform, person.RightArmTransform, 
                person.LeftFootAnchor, person.RightFootAnchor, person.BodyAnchorTransform);
        }

        private void SetTransforms(Transform spineTransform, Transform leftHandAnchorTransform, Transform rightHandAnchorTransform, 
            Transform leftFootAnchorTransform, Transform rightFootAnchorTransform, Transform bodyAnchorTransform)
        {
            _spineRotation.SetTransform(spineTransform);
            _leftHandAnchorPosition.SetTransform(leftHandAnchorTransform);
            _rightHandAnchorPosition.SetTransform(rightHandAnchorTransform);
            _rightFootAnchorPosition.SetTransform(rightFootAnchorTransform);
            _leftFootAnchorPosition.SetTransform(leftFootAnchorTransform);
            _bodyAnchorPosition.SetTransform(bodyAnchorTransform);
        }
    }
}
