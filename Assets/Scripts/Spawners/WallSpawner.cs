using System;
using System.Collections.Generic;
using UnityEngine;

public class WallSpawner : Spawner
{
    private int _index = 0;
    private float _wallSpeed = 0;
    private List<Wall> _walls;
    public Wall СurrentWall { get; private set; }

    public override event Action Spawned;
    public override event Action Destroyed;
    
    public void Initialize(List<Wall> walls, float speed)
    {
        _walls = walls;
        _wallSpeed = speed;
    }

    public void StartSpawn()
    {
        Spawn();
    }

    protected override bool CanSpawn() => 
        _index < _walls.Count && СurrentWall == null;

    protected override void Spawn()
    {
        СurrentWall = Instantiate(_walls[_index], transform.position, Quaternion.identity);
        СurrentWall.Initialize(_wallSpeed);
        Spawned?.Invoke();
        СurrentWall.Destroyed += Destroy;
        _index++;
    }

    protected override void Destroy()
    {
        Destroyed?.Invoke();
        if(CanSpawn())
            Spawn();
    }
}