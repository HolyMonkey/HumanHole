using UnityEngine;

namespace HumanHole.Scripts.ActiveRagdoll
{
    public class Foot : MonoBehaviour
    {
        [SerializeField] private ConfigurableJoint _leg;
        [SerializeField] private LayerMask _ground;

        public bool IsGrounded => _isGrounded;

        private bool _isGrounded = false;
        private bool _isActive = true;

        public void SetActive(bool active)
        {
            _isActive = active;
            if (_isActive)
            {
                if (_isGrounded)
                    ConnectToGround();
            }
            else
            {
                DisconnectFromGround();
            }
        }

        public void Relaxe()
        {
            JointDrive drive = new JointDrive();
            drive.positionSpring = 0;
            drive.positionDamper = 2;
            drive.maximumForce = 3.402823e+38f;

            _leg.angularXDrive = drive;
            _leg.angularYZDrive = drive;
        }

        private void FixedUpdate()
        {
            bool isGrounded = Physics.Raycast(new Ray(transform.position, Vector3.down), 0.2f, _ground);

            if (_isActive)
            {
                if (_isGrounded && !isGrounded)
                    DisconnectFromGround();
                else if (!_isGrounded && isGrounded)
                    ConnectToGround();
            }

            _isGrounded = isGrounded;
        }

        private void ConnectToGround()
        {
            JointDrive drive = new JointDrive();
            drive.positionSpring = 20;
            drive.positionDamper = 2;
            drive.maximumForce = 3.402823e+38f;

            _leg.angularXDrive = drive;
            _leg.angularYZDrive = drive;
        }

        private void DisconnectFromGround()
        {
            Relaxe();
        }
    }
}
