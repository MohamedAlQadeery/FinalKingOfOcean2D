using FishGame.Core;
using FishGame.Ships;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FishGame.Scenes
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] Transform shipPos1;
        [SerializeField] Transform shipPos2;
        [SerializeField] Transform shipPos3;

        [SerializeField] List<Ship> mainShips;
        [SerializeField] List<Ship> shipFromResources;
        private const string shipsFolderName = "Ships";

        private PlayFabShipData shipDataService;


        private void Awake()
        {
            shipDataService = PlayFabShipData.Instance;
            shipFromResources = GetShipsFromResourcesFolder();
        }


        private void Start()
        {
            GetUserMainShips();
        }
        private void GetUserMainShips()
        {
            shipDataService.GetMainShips();
        }



        public void OnGetPlayerMainShipsSuccess(List<SerializableShipData> playerMainShips)
        {
            List<Ship> mainShipsFromR = DeserialzeShipDataToShipList(playerMainShips);
            mainShips.Clear();
            mainShips.AddRange(mainShipsFromR);

            SpawnMainShips();
        }

        /**
      * Takes SerilziableShipData list and covert it to the Ship list from resoucrses folder
      */
        private List<Ship> DeserialzeShipDataToShipList(List<SerializableShipData> playerMainShips)
        {
            List<Ship> mainShipsFromR = new List<Ship>();
            foreach (SerializableShipData ship in playerMainShips)
            {
                if (FindScriptableObjectShip(ship.shipName) != null)
                {
                    mainShipsFromR.Add(FindScriptableObjectShip(ship.shipName));
                }
            }

            return mainShipsFromR;
        }

        private Ship FindScriptableObjectShip(string name)
        {
            foreach (Ship ship in shipFromResources)
            {
                if (ship.GetShipName().ToLower() == name.ToLower())
                {
                    return ship;
                }
            }
            return null;
        }

        private List<Ship> GetShipsFromResourcesFolder()
        {
            return Resources.LoadAll<Ship>(shipsFolderName).ToList();
        }

        private void SpawnMainShips()
        {
            if (mainShips.Count == 0) return;
            if (mainShips[0] != null)
            {
                mainShips[0].SpawnShip(shipPos1);
            }
            if (mainShips[1] != null)
            {
                mainShips[1].SpawnShip(shipPos2);
            }
            if (mainShips[2] != null)
            {
                mainShips[2].SpawnShip(shipPos3);
            }
          
        }
    }
}
