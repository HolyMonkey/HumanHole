using UnityEngine;

namespace HumanHole.Scripts.ActiveRagdoll
{
    public class Person : MonoBehaviour
    {
        private const float AdditionalBalanceValue = 0.5f;
        private const float DriveMaximumForce = 3.402823e+38f;
        private const int ActivePositionSpring = 20;
        private const int ActivePositionDamper = 5;
        private const int NotActivePositionXSpring = 10;
        private const int NotActivePositionDamperX = 2;
        
        [SerializeField] private Foot _leftFootAnchor;
        [SerializeField] private Foot _rightFootAnchor;
        [SerializeField] private Hand _leftArm;
        [SerializeField] private Hand _rightArm;
        [SerializeField] private Transform _leftHandAnchorTransform;
        [SerializeField] private Transform _rightHandAnchorTransform;
        [SerializeField] private Transform _bodyAnchorTransform;
        [SerializeField] private float _feetWidth = 0.3f;
        [SerializeField] private ConfigurableJoint _spine;
        [SerializeField] private bool _hidden = true;
        
        private CollisionObserver _collisionObserver;
        private BodyPart[] _parts;
        private float _balance = 0;
        private bool _isStabilization = true;

        public float Weight { get; private set; }
        public float Balance => _balance;
        public Head Head { get; private set; }
        
        public Transform LeftFootAnchor => _leftFootAnchor.transform;
        public Transform RightFootAnchor => _rightFootAnchor.transform;
        public Transform LeftArmTransform => _leftHandAnchorTransform;
        public Transform RightArmTransform => _rightHandAnchorTransform;
        public Transform BodyAnchorTransform => _bodyAnchorTransform;
        public Transform SpineTransform => _spine.transform;
    
        public void Initial(CollisionObserver collisionObserver)
        {
            _parts = GetComponentsInChildren<BodyPart>();
            _collisionObserver = collisionObserver;
        }

        public void Enable() => 
            enabled = true;

        private void OnEnable() => 
            _collisionObserver.WallCollidedPlayer += OnWallCollidedPlayer;

        private void OnDisable() => 
            _collisionObserver.WallCollidedPlayer -= OnWallCollidedPlayer;

        private void Start()
        {
            float weight = 0;
            foreach (var item in _parts)
            {
                weight += item.Mass;
                if (item is Head head)
                    Head = head;
            }

            Weight = weight;

            if (_hidden)
            {
                var meshes = GetComponentsInChildren<MeshRenderer>();
                foreach (var item in meshes)
                    item.enabled = false;
            }
        }

        private void OnWallCollidedPlayer() => 
            SetStabilization(false);

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.S)) 
                SetStabilization(!_isStabilization);
        }

        private void FixedUpdate() => 
            CalcCenterOfMass();

        private void CalcCenterOfMass()
        {
            Vector3 position = Vector3.zero;
            float totalMass = 0;
            foreach (var part in _parts)
            {
                position += part.Center * part.Mass;
                totalMass += part.Mass;
            }

            Vector3 result = position / totalMass;

            float balance = 0;
            if (_rightFootAnchor.IsGrounded && _leftFootAnchor.IsGrounded)
            {
                float minX = Mathf.Min(_rightFootAnchor.transform.position.x, _leftFootAnchor.transform.position.x);
                float maxX = Mathf.Max(_rightFootAnchor.transform.position.x, _leftFootAnchor.transform.position.x);
                float half = (maxX - minX) / 2;
                float midX = minX + half;

                float value = (result.x - midX) / half;
                balance = value * AdditionalBalanceValue;
            }
            else if (_rightFootAnchor.IsGrounded)
            {
                float half = _feetWidth / 2;
                float midX = _rightFootAnchor.transform.position.x;
                float value = (result.x - midX) / half;
                balance = value * AdditionalBalanceValue;
            }
            else if (_leftFootAnchor.IsGrounded)
            {
                float half = _feetWidth / 2;
                float midX = _leftFootAnchor.transform.position.x;
                float value = (result.x - midX) / half;
                balance = value * AdditionalBalanceValue;
            }

            _balance = balance;
        }

        private void SetStabilization(bool active)
        {
            _isStabilization = active;
            
            _rightFootAnchor.SetActive(active);
            _leftFootAnchor.SetActive(active);

            if (active)
            {
                JointDrive drive = CreateJointDrive(ActivePositionSpring,ActivePositionDamper, DriveMaximumForce);;
                _spine.angularXDrive = drive;
                _spine.angularYZDrive = drive;
            }
            else
            {
                _spine.angularXDrive = CreateJointDrive(NotActivePositionXSpring,NotActivePositionDamperX, DriveMaximumForce);;
                _spine.angularYZDrive = CreateJointDrive(0,0, DriveMaximumForce);
            }
        }

        public void Swim()
        {
            _leftArm.PrepareForSwim();
            _rightArm.PrepareForSwim();
        }
        
        private JointDrive CreateJointDrive(float positionSpring, float positionDamper, float maximumForce) =>
            new JointDrive
            {
                positionSpring = positionSpring,
                positionDamper = positionDamper,
                maximumForce = maximumForce
            };
    }
}
