using FishGame.Fishes;
using System;
using System.Collections.Generic;
using UnityEngine;
namespace FishGame.Ships
{
    
    [CreateAssetMenu(fileName = "Ship", menuName = "ScriptableObjects/Ships/Create New Ship", order = 0)]
    public class Ship : ScriptableObject
    {
        [SerializeField] GameObject shipPrefab;
        [SerializeField] SerializableShipData dataToJson;

        [SerializeField]  string shipName;
        [SerializeField] int currentCapacity, maxCapacity, unlockLevel;
        [SerializeField] float currentHealth, maxHealth, healDuration, healAmount, fishingDuration;
        [SerializeField] int numberFishCaughtPerMin;   
        
        [SerializeField] Sprite shipImage, shipIcon,destroyedImage;
        [SerializeField] bool canUpgrade;
       [SerializeField] public bool  isFishing,onCooldown;
        [SerializeField] List<Fish> canFishTypes; 
       [SerializeField] List<SerializableFishData> caughtFishes;
        [SerializeField] AnimatorOverrideController animatorOverrideController;

        //sell price is lower 
        [SerializeField] int sellPrice , buyPrice;

        /*private void OnDisable()
        {
            currentCapacity = 0;
            dataToJson.currentCapacity = 0;
            dataToJson.currentHealth = 0;
            dataToJson.Fishing = "";
            dataToJson.Stop = "";
            dataToJson.Xpos = 0 ;
            dataToJson.Ypos = 0 ;
            dataToJson.QuitTime = "";
            dataToJson.TimeToFill = "";
            dataToJson.FishType = -1;
           }*/


        public void SetCurrentCapacity(int cap)
        {
            currentCapacity = cap;
        }
        public int GetSellPrice()
        {
            return sellPrice;
        }

        public int GetBuyPrice()
        {
            return buyPrice;
        }

        public void SetCurrentHealth(float health)
        {
            currentHealth = health;
        }

        public float GetMaxHealth()
        {
            return maxHealth;
        }

        public void SpawnShip(Transform transform)
        {
            //Instantiate(shipPrefab, postion);
            GameObject  shipInstantiate= Instantiate(shipPrefab, transform.position, Quaternion.identity) as GameObject;
            shipInstantiate.transform.SetParent(GameObject.FindGameObjectWithTag("ShipUI").transform, false);
        }

        public  Sprite GetShipIcon()
        {
            return shipIcon;
        }

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
            //dataToJson.currentCapacity = caughtFishes.Count;
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

      

        public string GetShipName()
        {
            return shipName;
        }
    }


    [Serializable]
    public class SerializableShipData
    {
        public string shipName;
        public float currentHealth;

        public string QuitTime;
        public string TimeToFill;
        public int FishType;
        public int currentCapacity;
        public string Fishing;
        public string Stop;
        public float Xpos;
        public float Ypos;



        


       

    }

}