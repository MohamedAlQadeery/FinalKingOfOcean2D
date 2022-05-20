using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMarketPanel : MonoBehaviour
{
    
    public GameObject toolsScroll;
    public GameObject fishScroll;
    public GameObject upgradeButton;
    public GameObject toolInfo;

    public void FishButton()
    {
        toolsScroll.SetActive(false);
        fishScroll.SetActive(true);
    }

    public void ToolsButton()
    {
        fishScroll.SetActive(false);
        toolsScroll.SetActive(true);
    }

    public void UpgradeButton()
    {
        upgradeButton.SetActive(true);
    }

    public void UpgradeCloseButton()
    {
        upgradeButton.SetActive(false);
    }

    public void ToolInfo()
    {
        if (toolInfo.active)
        {
            toolInfo.SetActive(false);
        } 
        else if (!toolInfo.active)
        {
            toolInfo.SetActive(true);
        }
    }

}
