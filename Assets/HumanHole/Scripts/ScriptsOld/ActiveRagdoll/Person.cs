using System;
using UnityEngine;

public class Person : MonoBehaviour
{
    private CollisionObserver _collisionObserver;
    private BodyPart[] _parts;
    private float _balance = 0;
    private bool _isStabilizating = true;

    [SerializeField] private Foot[] _feet;
    [SerializeField] private Hand[] _hands;
    [SerializeField] private float _feetWidth = 0.3f;
    [SerializeField] private ConfigurableJoint _spine;
    [SerializeField] private bool _hidden = true;
    
    public float Weight { get; private set; }
    public float Balance => _balance;
    public Head Head { get; private set; }
    
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
        {
            SetStabilization(!_isStabilizating);
        }
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
        if (_feet[0].IsGrounded && _feet[1].IsGrounded)
        {
            float minX = Mathf.Min(_feet[0].transform.position.x, _feet[1].transform.position.x);
            float maxX = Mathf.Max(_feet[0].transform.position.x, _feet[1].transform.position.x);
            float half = (maxX - minX) / 2;
            float midX = minX + half;

            float value = (result.x - midX) / half;
            balance = value * 0.5f;
        }
        else if (_feet[0].IsGrounded)
        {
            float half = _feetWidth / 2;
            float midX = _feet[0].transform.position.x;
            float value = (result.x - midX) / half;
            balance = value * 0.5f;
        }
        else if (_feet[1].IsGrounded)
        {
            float half = _feetWidth / 2;
            float midX = _feet[1].transform.position.x;
            float value = (result.x - midX) / half;
            balance = value * 0.5f;
        }

        _balance = balance;
    }

    private void SetStabilization(bool active)
    {
        _isStabilizating = active;

        foreach (var item in _feet)
            item.SetActive(active);

        if (active)
        {
            JointDrive drive = new JointDrive();
            drive.positionSpring = 20;
            drive.positionDamper = 5;
            drive.maximumForce = 3.402823e+38f;

            _spine.angularXDrive = drive;
            _spine.angularYZDrive = drive;
        }
        else
        {
            JointDrive drive = new JointDrive();
            drive.positionSpring = 10;
            drive.positionDamper = 2;
            drive.maximumForce = 3.402823e+38f;

            JointDrive drive1 = new JointDrive();
            drive1.positionSpring = 0;
            drive1.positionDamper = 0;
            drive1.maximumForce = 3.402823e+38f;

            _spine.angularXDrive = drive;
            _spine.angularYZDrive = drive1;
        }
    }

    public void Swim()
    {
        foreach (var hand in _hands)
        {
            hand.PrepareForSwim();
        }
    }
}
