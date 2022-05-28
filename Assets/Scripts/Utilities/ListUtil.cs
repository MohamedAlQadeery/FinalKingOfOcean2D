using FishGame.Ships;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FishGame.Utilities
{
    public class ListUtil
    {
        private static ListUtil _instance;

        public static ListUtil Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ListUtil();

                }
                return _instance;
            }
        }
        ResourcesUtil resourcesUtil;
        public ListUtil()
        {
            _instance = this;
            resourcesUtil= ResourcesUtil.Instance;

        }


        public List<Ship> DeserialzeShipDataToShipList(List<SerializableShipData> serialzieShipsList)
        {
            List<Ship> shipsFromResources = new List<Ship>();
            foreach (SerializableShipData ship in serialzieShipsList)
            {
                if (resourcesUtil.FindScriptableObjectShip(ship.shipName) != null)
                {
                    shipsFromResources.Add(resourcesUtil.FindScriptableObjectShip(ship.shipName));
                }
            }

            return shipsFromResources;
        }




    }
}
