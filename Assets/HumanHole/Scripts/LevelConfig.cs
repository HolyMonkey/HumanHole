using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelConfig", menuName = "ScriptableObjects/LevelConfig", order = 1)]
public class LevelConfig : ScriptableObject
{
    public int LevelNumber;
    public float WallsSpeed;
    public int PointPerWall;
    [SerializeField] private List<WallIStaticData> _wallStaticData;
    public List<Wall> Walls => _wallStaticData.Select(x => x.Wall).ToList();
    public List<Sprite> Countours => _wallStaticData.Select(x => x.HoleCounterSprite).ToList();
}