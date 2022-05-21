using FishGame.Core;
using FishGame.Ships;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FishGame.Sandbox
{
    public class SandboxGameManager : MonoBehaviour
    {
        [SerializeField]  List<Ship> userShips;
        [SerializeField]  List<Ship> mainShips;
        private PlayFabShipData playFabShipDataService;
        private const string shipsFolderName = "Ships";
       [SerializeField] List<Ship> shipFromResources ;

        private void Start()
        {
            playFabShipDataService = PlayFabShipData.Instance;
            userShips = new List<Ship>();
            mainShips = new List<Ship>();
            shipFromResources = GetShipsFromResourcesFolder();
            GetAllPlayerShips();
            GetPlayerMainShips();
        }
        public void GetPlayerMainShips()
        {
            playFabShipDataService.GetMainShips();
        }

        public void UpdatePlayerMainShips()
        {
            playFabShipDataService.UpdateMainShips(mainShips);
        }

        public void GetAllPlayerShips()
        {
            playFabShipDataService.GetAllPlayerShips();            
        }

        public void OnGetPlayerMainShipsSuccess(List<SerializableShipData> playerMainShips)
        {
            List<Ship> mainShipsFromR = DeserialzeShipDataToShipList(playerMainShips);
            mainShips.Clear();
            mainShips.AddRange(mainShipsFromR);
        }

      
        public void OnUpdatePlayerMainShipsSuccess(string message)
        {
            Debug.Log("Main Ships is updated !!");
            GetPlayerMainShips();
        }

        public void OnGetAllPlayerShipsSuccess(List<SerializableShipData> ships)
        {
            Debug.Log("All Ships recieved successfully");
            List<Ship> allShipsFromR = DeserialzeShipDataToShipList(ships);
            
            userShips.Clear();
            userShips.AddRange(allShipsFromR);
        }

        public void OnError(string message)
        {
           
            Debug.Log($"From Sandbox Game Manager : {message}");
        }


        private Ship FindScriptableObjectShip(string name)
        {
            foreach(Ship ship in shipFromResources)
            {
                if(ship.GetShipName().ToLower() == name.ToLower())
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


        

    }
}
