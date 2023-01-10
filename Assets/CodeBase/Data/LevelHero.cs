using System;

namespace CodeBase.Data
{
    [Serializable]
    public class LevelHero
    {
        public const double modifier = 0.35;
        public const float startXP = 3500;

        public int CurrentLevel;

        public int xp;

        public int XPToNext;

        public LevelHero()
        {
            CurrentLevel = 1;
            xp = 0;
            CalculateXPToNext();
        }

        public void UpgradeLevel()
        {
            CurrentLevel += 1;
            xp -= XPToNext;
            CalculateXPToNext();
        }

        public void ReCalcLevels()
        {
            if (xp < XPToNext)
            {
                return;
            }
            
            UpgradeLevel();

            if (xp >= XPToNext)
            {
                ReCalcLevels();
            }
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