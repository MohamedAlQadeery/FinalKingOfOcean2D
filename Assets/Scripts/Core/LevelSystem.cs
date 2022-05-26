
using UnityEngine;
using UnityEngine.Events;

namespace FishGame.Core
{
    public class LevelSystem 
    {
        private static LevelSystem _instance;

        public static LevelSystem Instance
        {
            get {
                if (_instance == null)
                {
                    _instance = new LevelSystem();
                }
                return _instance;
            }
        }

        public LevelSystem()
        {
            _instance = this;
            _instance.level = 0;
            _instance.experience = 0;
        }


        public UnityEvent OnLevelChanged;
        public UnityEvent OnExperinceGained;

        // from level 1 to 10 
        private static readonly int[] experiencePerLevel = new[] { 100, 120, 140, 160, 180, 200, 220, 250, 300, 400 };
        private int level;
        private int experience;


        public void AddExperince(int amount)
        {
            if (!isMaxLevel())
            {
                experience += amount;
                while(!isMaxLevel() && experience>= GetExperinceToNextLevel(level))
                {
                    // we have enough experince to level up 
                    //we reset experince to 0 here and incremet the level
                    experience -= GetExperinceToNextLevel(level);
                    level++;
                    OnLevelChanged?.Invoke();
                }
                OnExperinceGained?.Invoke();
            }

            
        }


        public int GetCurrentExperince()
        {
            return experience;
        }
        public int GetCurrentLevel()
        {
            return level;
        }

        public int GetExperinceToNextLevel(int level)
        {
            if(level < experiencePerLevel.Length)
            {
                return experiencePerLevel[level];
            }
            Debug.Log($"Level is invalid {level}");
            return -1;
        }

        public bool isMaxLevel()
        {
            return isMaxLevel(level);
        }
       
        public bool isMaxLevel(int level)
        {
            return level == experiencePerLevel.Length - 1;
        }
        
    }

}