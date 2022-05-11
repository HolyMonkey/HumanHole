using System;

namespace HumanHole.Scripts.Data
{
    [Serializable]
    public class Progress
    {
        [NonSerialized] public bool IsGameStarted;

        public LevelsProgress LevelsProgress;
        public CharactersProgress CharactersProgress;
        public GoldProgress GoldProgress;
        public PointsProgress PointsProgress;
        public ShopProgress ShopProgress;

        public Progress()
        {
            LevelsProgress = new LevelsProgress();
            CharactersProgress = new CharactersProgress();
            GoldProgress = new GoldProgress();
            ShopProgress = new ShopProgress();
            PointsProgress = new PointsProgress();
        }
    }
}