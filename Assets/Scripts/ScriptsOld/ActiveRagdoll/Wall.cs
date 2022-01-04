using System;
using UnityEngine;

public class Wall : MonoBehaviour
{
    private float _speed = 2;

    private WallCollider[] _colliders;
    private Person _person;
    private int _index;
    private bool _touchedPerson;
    private bool _allowed;

    [SerializeField] private Sprite _contour;

    public Sprite Contour => _contour;

    public event Action TouchedPlayer;
    public event Action LeftPlayerZone;
    public event Action Destroyed;

    private void Awake()
    {
        _colliders = GetComponentsInChildren<WallCollider>();
    }

    public void Initialize(float speed)
    {
        _speed = speed;
        AllowMovement();
    }

    public void AllowMovement()
    {
        _allowed = true;
    }

    public void StopMovement()
    {
        _allowed = false;
    }

    private void OnEnable()
    {
        foreach (var item in _colliders)
            item.TouchedPerson += OnTouchedPerson;
    }

    private void OnDisable()
    {
        foreach (var item in _colliders)
            item.TouchedPerson -= OnTouchedPerson;
    }

    private void Update()
    {
        if (_allowed)
            transform.position += Vector3.back * _speed * Time.deltaTime;
    }

    private void OnTouchedPerson()
    {
        if (!_touchedPerson)
        {
            _touchedPerson = true;
            TouchedPlayer?.Invoke();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out LeftPlayerZone leftPlayerZone))
        {
            LeftPlayerZone?.Invoke();
        }
        else if (other.TryGetComponent(out DestroyWallZone destroyWallZone))
        {
            Destroyed?.Invoke();
            Destroy(gameObject);
        }
    }
}