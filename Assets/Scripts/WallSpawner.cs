using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSpawner : MonoBehaviour
{
    [SerializeField] private Wall[] _wallPrefabs;

    public Wall SpawnWall(int index)
    {
        return Instantiate(_wallPrefabs[index], transform.position, Quaternion.identity);
    }
}
