using FishGame.Core;
using FishGame.Ships;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UserShipsContent : MonoBehaviour
{
    [SerializeField] ShipBox shipTypePrefab;
    [SerializeField] Transform parentContent;

    List<Ship> userShips;
    PlayFabShipData shipDataService;
    [SerializeField] List<Ship> shipFromResources;
    private const string shipsFolderName = "Ships";




    private void Awake()
    {
        shipDataService = PlayFabShipData.Instance;
      
    }

    private void Start()
    {
        shipFromResources = GetShipsFromResourcesFolder();
        shipDataService.getUserShipsEventSuccess.AddListener(OnGetUserShipsSuccess);
        shipDataService.GetAllPlayerShips();


    }

    private void OnGetUserShipsSuccess(List<SerializableShipData> ships)
    {
        Debug.Log("inside OnGetUserShipsSuccess ()");
        userShips = DeserialzeShipDataToShipList(ships);
        foreach (var ship in userShips)
        {
            ShipBox shipItem = Instantiate(shipTypePrefab, parentContent);
            shipItem.SetShipBoxName(ship.GetShipName());
            shipItem.SetShipBoxPrice("2000");
            shipItem.SetShipImage(ship.GetShipIcon());

            
        }
    }

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
}
