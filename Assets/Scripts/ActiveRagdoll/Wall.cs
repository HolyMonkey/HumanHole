using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Wall : MonoBehaviour
{
    [SerializeField] private float _speed = 2;

    private WallCollider[] _colliders;

    public event UnityAction<Person> TouchedPerson;

    private void Awake()
    {
        _colliders = GetComponentsInChildren<WallCollider>();
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

    private void OnTouchedPerson(Person person)
    {
        TouchedPerson?.Invoke(person);
    }

    private void Update()
    {
        transform.position += Vector3.back * _speed * Time.deltaTime;
    }
}
