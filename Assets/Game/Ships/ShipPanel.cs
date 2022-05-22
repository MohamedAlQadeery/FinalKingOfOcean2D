using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipPanel : MonoBehaviour
{

    [SerializeField] GameObject shipsPanel;


    public void ShipsPanel()
    {
        shipsPanel.SetActive(true);
        //GameObject newUiSginUp = Instantiate(shipsPanel,new Vector3(0,0,0), transform.rotation) as GameObject;
        //newUiSginUp.transform.SetParent(this.transform, false);
    }

}
