using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipAnimations : MonoBehaviour
{

    public Animator fishAnim;
    Animator shipAnim;
    public GameObject shipPopUpPanel;

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
        fishAnim.SetBool("isFishing", true);
        shipAnim.SetBool("IsShipFishing", true);
        Close();
    }

    public void StopFishing()
    {
        fishAnim.SetBool("isFishing", false);
        shipAnim.SetBool("IsShipFishing", false);
        Close();
    }

    public void ShipInfo()
    {

    }

    public void Close()
    {
        shipPopUpPanel.SetActive(false);
    }
}

