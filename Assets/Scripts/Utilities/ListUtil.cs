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


        private List<Ship> DeserialzeShipDataToShipList(List<SerializableShipData> playerMainShips)
        {
            List<Ship> mainShipsFromR = new List<Ship>();
            foreach (SerializableShipData ship in playerMainShips)
            {
                if (resourcesUtil.FindScriptableObjectShip(ship.shipName) != null)
                {
                    mainShipsFromR.Add(resourcesUtil.FindScriptableObjectShip(ship.shipName));
                }
            }

            return mainShipsFromR;
        }




    }
}
