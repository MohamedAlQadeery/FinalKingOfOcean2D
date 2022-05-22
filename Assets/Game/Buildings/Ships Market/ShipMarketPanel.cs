using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMarketPanel : MonoBehaviour
{
    
    //public GameObject toolsScroll;
   // public GameObject fishScroll;
    public GameObject upgradePanel;
    public GameObject shipInfoPanel;

    //public void FishButton()
    //{
    ///    toolsScroll.SetActive(false);
    //    fishScroll.SetActive(true);
   // }

   // public void ToolsButton()
   /// {
    ///    fishScroll.SetActive(false);
    //    toolsScroll.SetActive(true);
   // }

    public void UpgradeButton()
    {
        upgradePanel.SetActive(true);
    }

    public void UpgradeCloseButton()
    {
        upgradePanel.SetActive(false);
    }

    public void ShipInfoButton()
    {
        shipInfoPanel.SetActive(true);
    }

    public void ShipInfoCloseButton()
    {
        shipInfoPanel.SetActive(false);
    }

}
