using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipAnimations : MonoBehaviour
{

    public Animator fishAnim;
    //Animator shipAnim;
    public GameObject shipPopUpPanel;
    public GameObject fishingRuning;
    public GameObject fishingStoped;
    public GameObject infoPanel;
    public GameObject shipPopUpPanelAnimation;

    private void Start()
    {
        //shipAnim = GetComponent<Animator>();
    }

    public void ShipsPanel()
    {
        shipPopUpPanel.SetActive(true);
        LeanTween.scale(shipPopUpPanelAnimation, new Vector3(0.1089657f, 0.06674148f, 0f), 0.5f);
    }
    public void StartFishing()
    {
        LeanTween.moveX(gameObject, transform.position.x + 500, 2);
        LeanTween.scaleX(gameObject , -4 , 0.4f);
        fishingRuning.SetActive(true);
        fishingStoped.SetActive(false);
        fishAnim.SetBool("isFishing", true);
        //shipAnim.SetBool("IsShipFishing", true);
        StartCoroutine("ShipRotate");
        Close();
    }

    IEnumerator ShipRotate()
    {
        yield return new WaitForSeconds(2);
        LeanTween.scaleX(gameObject, 4, 0.4f);
    }

    public void StopFishing()
    {        
        LeanTween.moveX(gameObject, transform.position.x-500, 2);
        fishingRuning.SetActive(false);
        fishingStoped.SetActive(true);
        fishAnim.SetBool("isFishing", false);
        //shipAnim.SetBool("IsShipFishing", false);
        Close();
    }

    public void ShipInfo()
    {
        infoPanel.SetActive(true);
    }

    public void Close()
    {
        LeanTween.scale(shipPopUpPanelAnimation, new Vector3(0, 0, 0), 0.5f);
        StartCoroutine("EndAnime");
    }
    IEnumerator EndAnime()
    {
        yield return new WaitForSeconds(0.5f);
        shipPopUpPanel.SetActive(false);

    }

    public void clsoeInfo() 
    {
        infoPanel.SetActive(false);
    }

}

