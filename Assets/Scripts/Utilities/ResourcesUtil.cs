using FishGame.Fishes;
using FishGame.Ships;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FishGame.Utilities
{
    public class ResourcesUtil 
    {
        private const string shipsFolderName = "Ships";
        private const string fishesFolderName = "Fishses";
    
        private static ResourcesUtil _instance;

        public static ResourcesUtil Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ResourcesUtil();
                    
                }
                return _instance;
            }
        }

        public ResourcesUtil()
        {
            _instance = this;
            

        }


        private List<Ship> GetShipsFromResourcesFolder()
        {
            return Resources.LoadAll<Ship>(shipsFolderName).ToList();
        }

        public List<Fish> GetFishFromResourcesFolder()
        {
            return Resources.LoadAll<Fish>(fishesFolderName).ToList();

        }

        public Ship FindScriptableObjectShip(string name)
        {
            foreach (Ship ship in GetShipsFromResourcesFolder())
            {
                if (ship.GetShipName().ToLower() == name.ToLower())
                {
                    return ship;
                }
            }
            return null;
        }
    }
}

