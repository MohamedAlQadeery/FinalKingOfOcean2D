using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMarketPanel : MonoBehaviour
{
    
    public GameObject shipMarketScroll;
    public GameObject shipStoreScroll;
    public GameObject upgradePanel;
    public GameObject shipInfoPanel;
    public GameObject ConfirmSellPanel;

    public void ShipStoreButton()
    {
        shipMarketScroll.SetActive(false);
        shipStoreScroll.SetActive(true);
    }

    public void ShipMarketButton()
    {
        shipStoreScroll.SetActive(false);
        shipMarketScroll.SetActive(true);
    }

    public void UpgradeButton()
    {
        upgradePanel.SetActive(true);
    }

    public void UpgradeCloseButton()
    {
        upgradePanel.SetActive(false);
    }

  

    public void ShipInfoCloseButton()
    {
        shipInfoPanel.SetActive(false);
    }


    public void DisplayConfirmMessage()
    {
        ConfirmSellPanel.SetActive(true);
    }
}
