using FishGame.Core;
using FishGame.Ships;
using FishGame.Utilities;
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

        [SerializeField] List<Ship> ownedShips;
        [SerializeField] List<Ship> shipFromResources;

        private PlayFabShipData shipDataService;
        private static bool isOwnedShipsModifed = false;
        private static bool isOwnedShipsSpawned = false;

        private void Awake()
        {
            shipDataService = PlayFabShipData.Instance;
            shipFromResources = ResourcesUtil.Instance.GetShipsFromResourcesFolder();
            shipDataService.getOwnedShipsListEventSuccess.AddListener(OnGetPlayerOwnedShipsSuccess);

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
            Debug.Log("Inside OnGetPlayerOwnedShipsSuccess()");
            List<Ship> ownedShipsFromResources = ListUtil.Instance.DeserialzeShipDataToShipList(playerOwnedShips);
            ownedShips.Clear();
            ownedShips.AddRange(ownedShipsFromResources);

            SpawnMainShips();
        }



        private void SpawnMainShips()
        {
            if (ownedShips.Count == 0) return;
            if (ownedShips[0] != null)
            {
                ownedShips[0].SpawnShip(shipPos1);
            }
            if (ownedShips[1] != null)
            {
                ownedShips[1].SpawnShip(shipPos2);
            }
            if (ownedShips[2] != null)
            {
                ownedShips[2].SpawnShip(shipPos3);
            }
            isOwnedShipsSpawned = true;

        }
    }
}
