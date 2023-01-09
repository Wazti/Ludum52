using System;

namespace CodeBase.Data
{
    [Serializable]
    public class LevelHero
    {
        public const double modifier = 0.1;
        public const float startXP = 15000;

        public int CurrentLevel;

        public int xp;

        public int XPToNext;

        public LevelHero()
        {
            CurrentLevel = 3;
            xp = 2000;
            CalculateXPToNext();
        }

        public void UpgradeLevel()
        {
            CurrentLevel += 1;
            xp = 0;
        }

        public void CalculateXPToNext()
        {
            XPToNext = CalculateXPToLevel(CurrentLevel - 1);
        }

        public int CalculateXPToLevel(int level)
        {
            if (level < 0) return 0;

            return (int) (startXP * Math.Pow(1 + modifier, level));
        }

        public float Progress()
        {
            return xp / (float) XPToNext;
        }
    }
}