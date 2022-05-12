using System;
using System.Collections.Generic;
using System.Linq;
using HumanHole.Scripts.Level;
using UnityEngine;

namespace HumanHole.Scripts.LevelLogic
{
    [CreateAssetMenu(fileName = "LevelsStaticData", menuName = "StaticData/LevelsStaticData")]
    public class LevelsStaticData : ScriptableObject
    {
        [SerializeField] private List<LevelStaticData> _levelsStaticData;

        public IReadOnlyList<LevelStaticData> LevelsStaticDataList => _levelsStaticData;

        public LevelStaticData GetLevelStaticDataByNumber(int number)
        {
            LevelStaticData levelStaticData = _levelsStaticData.FirstOrDefault(x => x.Number == number);
            if (levelStaticData == null)
                throw new NullReferenceException($"Level static data with number {number} is null");

            return levelStaticData;
        }
    }
}
