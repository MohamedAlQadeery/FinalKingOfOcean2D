using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipAnimations : MonoBehaviour
{

    public Animator fishAnim;
    Animator shipAnim;
    public GameObject shipPopUpPanel;
    public GameObject fishingRuning;
    public GameObject fishingStoped;
    public GameObject infoPanel;

    private void Start()
    {
        shipAnim = GetComponent<Animator>();
    }

    public void ShipsPanel()
    {
        shipPopUpPanel.SetActive(true);
    }
        public void StartFishing()
    {
        fishingRuning.SetActive(true);
        fishingStoped.SetActive(false);
        fishAnim.SetBool("isFishing", true);
        shipAnim.SetBool("IsShipFishing", true);
        Close();
    }

    public void StopFishing()
    {
        fishingRuning.SetActive(false);
        fishingStoped.SetActive(true);
        fishAnim.SetBool("isFishing", false);
        shipAnim.SetBool("IsShipFishing", false);
        Close();
    }

    public void ShipInfo()
    {
        infoPanel.SetActive(true);

    }

    public void Close()
    {
        shipPopUpPanel.SetActive(false);
    }

    public void clsoeInfo() 
    {
        infoPanel.SetActive(false);
    }

}

