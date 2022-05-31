using FishGame.Core;
using FishGame.Ships;
using FishGame.Utilities;
using System;
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

        public  static List<Ship> ownedShips;
        public  static List<Ship> shipFromResources;

        private PlayFabShipData shipDataService;
        public static bool isOwnedShipsModifed = false;
        public static bool isOwnedShipsSpawned = false;

        private void Awake()
        {
            shipDataService = PlayFabShipData.Instance;
            shipFromResources = ResourcesUtil.Instance.GetShipsFromResourcesFolder();
            shipDataService.getOwnedShipsListEventSuccess.AddListener(OnGetPlayerOwnedShipsSuccess);
            shipDataService.updateOwnedShipsSuccessEvent.AddListener(OnUpdatedOwnedShipsSuccess);
            ownedShips = new List<Ship>();
            shipFromResources = new List<Ship>();
        }

        private void OnUpdatedOwnedShipsSuccess(string arg0)
        {
            Debug.LogError($"Inside OnUpdatedOwnedShipsSuccess in GameManager.cs");
            shipDataService.GetOwnedShips();

        }

        private void Start()
        {
            GetUserOwnedShips();
        }

        private void GetUserOwnedShips()
        {

            shipDataService.GetOwnedShips();
        }



        public void OnGetPlayerOwnedShipsSuccess(List<SerializableShipData> playerOwnedShips)
        {
            // if owned ships is spwaned  and its not modifed 
            if (isOwnedShipsSpawned && !isOwnedShipsModifed) return;
            Debug.Log("Inside OnGetPlayerOwnedShipsSuccess() in GameManager.cs");
            List<Ship> ownedShipsFromResources = ListUtil.Instance.DeserialzeShipDataToShipList(playerOwnedShips);
            ownedShips.Clear();
            ownedShips.AddRange(ownedShipsFromResources);

            SpawnMainShips();
            isOwnedShipsModifed = false;
        }



        private void SpawnMainShips()
        {
            if (ownedShips.Count == 0) return;
            DestroySpawnedShips();

            if (ownedShips.Count >= 1)
            {
                ownedShips[0].SpawnShip(shipPos1);
            }
            if (ownedShips.Count >= 2)
            {
                ownedShips[1].SpawnShip(shipPos2);
            }
            if (ownedShips.Count == 3)
            {
                ownedShips[2].SpawnShip(shipPos3);
            }
            isOwnedShipsSpawned = true;

        }

        private static void DestroySpawnedShips()
        {
            if (isOwnedShipsModifed)
            {
                foreach (var spanwedShip in GameObject.FindGameObjectsWithTag("Ship"))
                {
                    Destroy(spanwedShip);
                }
                //spawned Ships are destroyed
            }
        }

        public static List<Ship> GetOwnedShipsList()
        {
            return ownedShips;
        }
    }
}
