using UnityEngine;

public class LookAtPerson : MonoBehaviour
{
    [SerializeField] private СollisionObserver _collisionObserver;
    [SerializeField] private Person _person;
    [SerializeField] private Transform _mainCameraLookPoint;
    
    private Transform _target;
    private Vector3 _velocity;
    private Vector3 _lastLookPoint;

    private void OnEnable()
    {
        _collisionObserver.WallHitPerson += OnWallHitPerson;
    }

    private void OnDisable()
    {
        _collisionObserver.WallHitPerson -= OnWallHitPerson;
    }

    private void OnWallHitPerson()
    {
        SetTarget(_person.Head.transform);
    }

    public void SetTarget(Transform target)
    {
        if (_target == null)
            _lastLookPoint = target.transform.position;

        _target = target;
    }

    private void Update()
    {
        if (_target == null)
            return;

        _lastLookPoint = Vector3.SmoothDamp(_lastLookPoint, _target.position, ref _velocity, 0.2f);

        transform.rotation = Quaternion.LookRotation(_lastLookPoint - transform.position, Vector3.up);
    }
}
