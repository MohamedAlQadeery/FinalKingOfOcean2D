using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipsPopup : MonoBehaviour
{
    Animator fishAnim;
    Animator shipAnim;
    private void Start()
    {
        fishAnim = GameObject.FindGameObjectWithTag("ShipUI1").GetComponentInChildren<Animator>();
        shipAnim = GameObject.FindGameObjectWithTag("ShipUI").GetComponentInChildren<Animator>();
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
        Destroy(gameObject);
    }
}
