using FishGame.Ships;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using FishGame.Core;
using UnityEngine.UI;

public class ShipUIManager : MonoBehaviour
{

    public Animator fishAnim;
    public GameObject shipPopUpPanel;
    public GameObject fishingRuning;
    public GameObject fishingStoped;
    public GameObject infoPanel;
    public GameObject startFishingNavigation;
    public Ship ship;
    public GameObject confirmStopFishing;
    public TMP_Text fishNumber;

    #region Server Region
    public static Action<SerializableShipData> OnPaused;
    public static Action<string> OnBack;

    #endregion



    private void Awake()
    {
      //  PlayFabShipData.OnUpdatedShipData += HandleOnUpdatedShipData;
      

    }

    private void Start()
    {
      //  OnBack?.Invoke(ship.GetShipName());

        if (ship.GetDataToJson().Fishing == "true")
        {
            //float xPos = ship.GetDataToJson().Xpos - 311;
            Debug.Log($"Start method fishing is true x pos = {ship.GetDataToJson().Xpos}");
            transform.position = new Vector3(ship.GetDataToJson().Xpos, transform.position.y, 0);
            fishingRuning.SetActive(true);
            fishingStoped.SetActive(false);
            fishAnim.SetBool("isFishing", true);
        }
    }
    private void OnDestroy()
    {
        //PlayFabShipData.OnUpdatedShipData -= HandleOnUpdatedShipData;
    }

    private void HandleOnUpdatedShipData(SerializableShipData obj)
    {
        Debug.Log("HandleUpdateShipData IN UI MANEGER SHIP");

        ship.GetDataToJson().Xpos = obj.Xpos;
        ship.GetDataToJson().Ypos = obj.Ypos;
        ship.GetDataToJson().Fishing = obj.Fishing;
        ship.GetDataToJson().Stop = obj.Stop;      
    }
    /*we need all this as playerPrefs
   1- Position on fishing and on stop (2)
   2- fishingRuning.SetActive()/fishingStoped.SetActive() that when locked a start fishing or stop fishing
   3- Time ofc
   4- fishAnim.SetBool("isFishing", true); fishing animations for the ## 4baka ~~'
   5- isFishing !!
   6- numper off fish and how incresi it when game glosed hard as shit @@*/
    public void StartFishing()
    {        
        GameObject startFishingNavigation1 = Instantiate(startFishingNavigation, transform.position, transform.rotation) as GameObject;
        startFishingNavigation1.transform.SetParent(GameObject.FindGameObjectWithTag("GameUI").transform, false);
        gameObject.GetComponent<Button>().enabled = false;
        Destroy(startFishingNavigation1, 2);
        LeanTween.moveX(gameObject, transform.position.x + 500, 3);
        LeanTween.scaleX(gameObject, -4, 0.4f);
        fishingRuning.SetActive(true);
        fishingStoped.SetActive(false);
        fishAnim.SetBool("isFishing", true);
        ship.isFishing = true;
        StartCoroutine("ShipRotate");
        StartCoroutine("EnabledClick");
        Close();
    }

    public void ConfirmStop()
    {
        gameObject.GetComponent<Button>().enabled = false;
        confirmStopFishing.SetActive(false);
        LeanTween.moveX(gameObject, transform.position.x - 500, 3);
        fishingRuning.SetActive(false);
        fishingStoped.SetActive(true);
        fishAnim.SetBool("isFishing", false);
        ship.isFishing = false;
        StartCoroutine("EnabledClick");
        Close();
    }

    public void StopFishing()
    {
        confirmStopFishing.SetActive(true);
        fishNumber.text = ship.GetCapacity().ToString();
    }
    // we dont need any off this down methods for save i think 
    IEnumerator ShipRotate()
    {
        yield return new WaitForSeconds(3);
        LeanTween.scaleX(gameObject, 4, 0.4f);
    }
    IEnumerator EnabledClick()
    {
        yield return new WaitForSeconds(3);
        gameObject.GetComponent<Button>().enabled = true;
    }
        public void ShipsPanel()
    {
        shipPopUpPanel.SetActive(true);
        LeanTween.scale(shipPopUpPanel, new Vector3(0.1f, 0.05f, 0f), 0.5f);
    }

    public void Close()
    {
        LeanTween.scale(shipPopUpPanel, new Vector3(0, 0, 0), 0.5f);
        StartCoroutine("EndAnime");
    }
    IEnumerator EndAnime()
    {
        yield return new WaitForSeconds(0.5f);
        shipPopUpPanel.SetActive(false);
    }

    public void ShipInfo()
    {
        infoPanel.SetActive(true);
    }

    public void clsoeInfo()
    {
        infoPanel.SetActive(false);
    }

    public void CloseConfirm()
    {
        confirmStopFishing.SetActive(false);
    }

}
