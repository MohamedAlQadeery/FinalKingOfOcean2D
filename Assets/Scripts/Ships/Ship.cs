using FishGame.Fishes;
using System;
using System.Collections.Generic;
using UnityEngine;
namespace FishGame.Ships
{
    
    [CreateAssetMenu(fileName = "Ship", menuName = "ScriptableObjects/Ships/Create New Ship", order = 0)]
    public class Ship : ScriptableObject
    {
        [SerializeField] SerializableShipData dataToJson;

        [SerializeField]  string shipName;
        [SerializeField] int currentCapacity, maxCapacity, unlockLevel;
        [SerializeField] float currentHealth, maxHealth, healDuration, healAmount, fishingDuration;
        [SerializeField] int numberFishCaughtPerMin;   
        
        [SerializeField] Sprite shipImage, shipIcon,destroyedImage;
        [SerializeField] bool canUpgrade;
       [SerializeField] bool  isFishing,onCooldown;
        [SerializeField] List<Fish> canFishTypes; 
       [SerializeField] List<SerializableFishData> caughtFishes;
        [SerializeField] AnimatorOverrideController animatorOverrideController;

        // main ships are the ships that is displayed at game scene (maximum 3 ships)
       [SerializeField] bool isMainShip = false;
        public bool CanFish()
        {
            if (currentHealth <= 0) return false;
            if (IsCapacityFull()) return false;
            if (isFishing) return false;
            if (onCooldown) return false;

            return true;
        }

        public bool IsCapacityFull()
        {
            return caughtFishes.Count == maxCapacity;
        }

        public int GetMaxCapacity()
        {
            return maxCapacity;
                
        }

        public List<Fish> GetCanFishTypesList()
        {
            return canFishTypes;
        }

        public List<SerializableFishData> GetCaughtFishList()
        {
            return caughtFishes;
        }
        
        public int GetCapacity()
        {
            return caughtFishes.Count;
        }

        public void ClearCapacity()
        {
            caughtFishes.Clear();
            currentCapacity = 0;
            dataToJson.currentCapacity = 0;

        }

        public void UpdateCurrentCapacity()
        {
            currentCapacity = caughtFishes.Count;
            dataToJson.currentCapacity = caughtFishes.Count;
        }

        public SerializableShipData GetDataToJson()
        {
            return dataToJson;
        }

        public int GetNumberOfFishCaughtPerMin()
        {
            return numberFishCaughtPerMin;
        }

        public float GetFishingDuration()
        {
            return fishingDuration;
        }

        public void SetIsMainShip(bool value)
        {
            isMainShip = value;
            dataToJson.isMainShip = value;
        }

        public string GetShipName()
        {
            return shipName;
        }
    }


    [Serializable]
    public class SerializableShipData
    {
        public string shipName;
        public int currentCapacity;
        public float currentHealth;
        public bool isMainShip;



    }

}