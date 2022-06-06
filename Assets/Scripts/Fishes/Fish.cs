using FishGame.Ships;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FishGame.Fishes
{
 
    [CreateAssetMenu(fileName = "Fish", menuName = "ScriptableObjects/Fish/Create New Fish", order = 1)]
  
    public class Fish : ScriptableObject
    {

        [SerializeField] SerializableFishData dataToJson;
         [SerializeField] string fishName;
        [SerializeField] Sprite fishIcon;
        [SerializeField] float maxPrice, minPrice, currentPrice;
        [SerializeField] float expiredDuration, apperanceRate;
        [SerializeField] float expPoints;

        public SerializableFishData GetDataToJson()
        {
            return dataToJson;
        }

       public string GetName()
        {
            return fishName;
        }

        public Sprite GetFishIcon()
        {
            return fishIcon;
        }

        public float GetCurrentPrice()
        {
            return currentPrice;
        }
    }

    public class SuperFish : Fish
    {
        List<Ship> comboShips;
    }

    [Serializable]
    public class SerializableFishData
    {
        public string fishName;
        public float expiredDuration ;
        public float expPoints;

    }

}