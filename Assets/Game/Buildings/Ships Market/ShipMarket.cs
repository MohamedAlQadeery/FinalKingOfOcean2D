using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMarket : MonoBehaviour
{
    public GameObject upgradeButton;
    public GameObject informationButton;

    public void UpgradeButton() {
        upgradeButton.SetActive(true);
    }

    public void UpgradeCloseButton() {
        upgradeButton.SetActive(false);
    }
    public void InformationButton() {
        informationButton.SetActive(true);
    }
    public void InformationCloseButton() {
        informationButton.SetActive(false);
    }
}
