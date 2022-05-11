using System;
using System.Collections.Generic;
using UnityEngine;

namespace HumanHole.Scripts.Wall
{
    [Serializable]
    public class LevelWallsStaticData
    {
        [SerializeField] private float _wallSpeed = 1;
        [SerializeField] private List<Wall> _walls;
        [SerializeField] private Color _color;

        public float WallSpeed => _wallSpeed;
        public IReadOnlyList<Wall> Walls => _walls;
        public Color Color => _color;
    }
}
