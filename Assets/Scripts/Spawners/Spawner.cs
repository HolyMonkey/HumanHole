using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spawner : MonoBehaviour
{
    protected abstract bool CanSpawn();
    protected abstract void Spawn();
    protected abstract void Destroy();
    public abstract event Action Spawned;
    public abstract event Action Destroyed;
}
