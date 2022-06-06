using FishGame.Fishes;
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


        public List<SerializableShipData> ConvertToSerializableShipList(List<Ship> list)
        {
            List<SerializableShipData> tmpList = new List<SerializableShipData>();
            foreach (Ship ship in list)
            {
                tmpList.Add(ship.GetDataToJson());
            }

            return tmpList;
        }

        public Dictionary<Fish,int> ConvertToStringKeyToFishType(Dictionary<string,int> fishDic)
        {
            Dictionary<Fish, int> newFishDic = new Dictionary<Fish, int>();

            foreach (var item in fishDic)
            {
                if(resourcesUtil.FindScriptableObjectFish(item.Key) != null)
                {
                    newFishDic.Add(resourcesUtil.FindScriptableObjectFish(item.Key),item.Value);
                }
            }

            return newFishDic;
        }


    }
}
