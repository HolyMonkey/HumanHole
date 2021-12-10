using System;
using UnityEngine;

public class Wall : MonoBehaviour
{
    private float _speed = 2;

    private WallCollider[] _colliders;
    private Person _person;
    private int _index;
    private bool _touchedPerson;

    public event Action TouchedPerson;
    public event Action Destroyed;

    private void Awake()
    {
        _colliders = GetComponentsInChildren<WallCollider>();
    }

    public void Initialize(float speed)
    {
        _speed = speed;
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
        transform.position += Vector3.back * _speed * Time.deltaTime;
    }

    private void OnTouchedPerson()
    {
        if (!_touchedPerson)
        {
            _touchedPerson = true;
            TouchedPerson?.Invoke();
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out DestroyWallZone destroyWallZone))
        {
            Destroyed?.Invoke();
            Destroy(gameObject);
        }
    }
}
