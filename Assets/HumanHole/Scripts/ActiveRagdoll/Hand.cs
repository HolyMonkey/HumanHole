using UnityEngine;

namespace HumanHole.Scripts.ActiveRagdoll
{
    public class Hand : MonoBehaviour
    {
        private const float DriveMaximumForce = 3.402823e+38f;
        private const int SwimPositionSpring = 20;
        private const int SwimPositionDamper = 2;

        [SerializeField] private ConfigurableJoint _upperArm;
        [SerializeField] private ConfigurableJoint _lowerArm;

        public void PrepareForSwim()
        {
            JointDrive drive = CreateJointDrive(SwimPositionSpring, SwimPositionDamper, DriveMaximumForce);
            SetSlerpDrive(drive);
        }

        public void Relax()
        {
            JointDrive drive = CreateJointDrive(0, 0, DriveMaximumForce);
            SetSlerpDrive(drive);
        }

        private JointDrive CreateJointDrive(float positionSpring, float positionDamper, float maximumForce) =>
            new JointDrive
            {
                positionSpring = positionSpring,
                positionDamper = positionDamper,
                maximumForce = maximumForce
            };

        private void SetSlerpDrive(JointDrive drive)
        {
            _upperArm.slerpDrive = drive;
            _lowerArm.slerpDrive = drive;
        }
    }
}
