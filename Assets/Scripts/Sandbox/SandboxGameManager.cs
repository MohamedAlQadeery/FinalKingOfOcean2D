using FishGame.Core;
using FishGame.Ships;
using FishGame.Utilities;
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
        ListUtil listUtilService;


        private void Start()
        {
            playFabShipDataService = PlayFabShipData.Instance;
            userShips = new List<Ship>();
            mainShips = new List<Ship>();
            GetAllPlayerShips();
            GetPlayerMainShips();
            listUtilService = ListUtil.Instance;

        }
        public void GetPlayerMainShips()
        {
            playFabShipDataService.GetOwnedShips();
        }

        public void UpdatePlayerMainShips()
        {
            playFabShipDataService.UpdateOwnedShips(mainShips);
        }

        public void GetAllPlayerShips()
        {
            playFabShipDataService.GetAllShips();            
        }

        public void OnGetPlayerMainShipsSuccess(List<SerializableShipData> playerMainShips)
        {
            List<Ship> mainShipsFromR = listUtilService.DeserialzeShipDataToShipList(playerMainShips);
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
            List<Ship> allShipsFromR = listUtilService.DeserialzeShipDataToShipList(ships);
            
            userShips.Clear();
            userShips.AddRange(allShipsFromR);
        }

        public void OnError(string message)
        {
           
            Debug.Log($"From Sandbox Game Manager : {message}");
        }



        

    }
}
