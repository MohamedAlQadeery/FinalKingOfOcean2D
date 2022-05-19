using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipPanel : MonoBehaviour
{

    [SerializeField] GameObject shipsPanel;

    public void ShipsPanel()
    {
        GameObject newUiSginUp = Instantiate(shipsPanel, transform.position, transform.rotation) as GameObject;
        newUiSginUp.transform.SetParent(GameObject.FindGameObjectWithTag("ShipUI").transform, false);
    }

}
