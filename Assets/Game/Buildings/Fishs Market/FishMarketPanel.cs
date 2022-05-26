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
        SoundManager.Instance.PlaySound(SoundManager.Sound.ButtonSonud);
        toolsScroll.SetActive(false);
        fishScroll.SetActive(true);
    }

    public void ToolsButton()
    {
        SoundManager.Instance.PlaySound(SoundManager.Sound.ButtonSonud);
        fishScroll.SetActive(false);
        toolsScroll.SetActive(true);
    }

    public void UpgradeButton()
    {
        SoundManager.Instance.PlaySound(SoundManager.Sound.ButtonSonud);
        upgradeButton.SetActive(true);
    }

    public void UpgradeBuildingButton()
    {
        SoundManager.Instance.PlaySound(SoundManager.Sound.UpgradeBuildingSound);
    }

    public void UpgradeCloseButton()
    {
        SoundManager.Instance.PlaySound(SoundManager.Sound.CloseSonud);
        upgradeButton.SetActive(false);
    }

    public void ToolInfo()
    {
        if (toolInfo.active)
        {
            SoundManager.Instance.PlaySound(SoundManager.Sound.ButtonSonud);
            toolInfo.SetActive(false);
        } 
        else if (!toolInfo.active)
        {
            SoundManager.Instance.PlaySound(SoundManager.Sound.CloseSonud);
            toolInfo.SetActive(true);
        }
    }

}
