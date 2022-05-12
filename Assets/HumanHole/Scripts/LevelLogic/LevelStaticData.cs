using HumanHole.Scripts.Wall;
using UnityEngine;

namespace HumanHole.Scripts.Level
{
    [CreateAssetMenu(fileName = "LevelStaticData", menuName = "StaticData/LevelStaticData")]
    public class LevelStaticData : ScriptableObject
    {
        [SerializeField] private int _number;
        [SerializeField] private int _levelWonGold;
        [SerializeField] private int _levelLoseGold;
        [SerializeField] private int _levelWonPoints;
        [SerializeField] private int _levelLosePoints;
        [SerializeField] private LevelWallsStaticData levelWallsStaticData;
        [SerializeField] private Color _platformColor;
        [SerializeField] private Material _waterMaterial;
        [SerializeField] private Material _skyBoxMaterial;

        public int Number => _number;
        public LevelWallsStaticData LevelWallsStaticData => levelWallsStaticData;
        public Color PlatformColor => _platformColor;
        public int LevelWonGold => _levelWonGold;
        public int LevelLoseGold => _levelLoseGold;
        public int LevelWonPoints => _levelWonPoints;
        public int LevelLosePoints => _levelLosePoints;
        public Material WaterMaterial => _waterMaterial;
        public Material SkyBoxMaterial => _skyBoxMaterial;
    }
}
