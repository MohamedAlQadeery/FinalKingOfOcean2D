using FishGame.Ships;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using FishGame.Core;

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
        PlayFabShipData.OnUpdatedShipData += HandleOnUpdatedShipData;
        OnBack?.Invoke(ship.GetShipName());

    }

    private void OnDestroy()
    {
        PlayFabShipData.OnUpdatedShipData -= HandleOnUpdatedShipData;

    }

    private void HandleOnUpdatedShipData(SerializableShipData obj)
    {
        ship.GetDataToJson().Xpos = obj.Xpos;
        ship.GetDataToJson().Ypos = obj.Ypos;
        ship.GetDataToJson().Fishing = obj.Fishing;
        ship.GetDataToJson().Stop = obj.Stop;

       
    }

    /*
we need all this as playerPrefs
   1- Position on fishing and on stop (2)
   2- fishingRuning.SetActive()/fishingStoped.SetActive() that when locked a start fishing or stop fishing
   3- Time ofc
   4- fishAnim.SetBool("isFishing", true); fishing animations for the ## 4baka ~~'
   5- isFishing !!
   6- numper off fish and how incresi it when game glosed hard as shit @@
*/
    private void Start()
    {
        if (PlayerPrefs.GetFloat(ship.GetShipName() + "Xpos") != 0) {
            float xPos = PlayerPrefs.GetFloat(ship.GetShipName() + "Xpos");
            float yPos = PlayerPrefs.GetFloat(ship.GetShipName() + "Ypos");
            transform.position = new Vector3(xPos, yPos, 0);
            if (PlayerPrefs.GetString(ship.GetShipName() + "Fishing").Equals("true"))
            {
                fishingRuning.SetActive(true);
                fishingStoped.SetActive(false);
                fishAnim.SetBool("isFishing", true);
            }
        }
    }
    public void StartFishing()
    {

        GameObject startFishingNavigation1 = Instantiate(startFishingNavigation, transform.position, transform.rotation) as GameObject;
        startFishingNavigation1.transform.SetParent(GameObject.FindGameObjectWithTag("GameUI").transform, false);
        Destroy(startFishingNavigation1, 2);
        LeanTween.moveX(gameObject, transform.position.x + 300, 2);
        LeanTween.scaleX(gameObject, -4, 0.4f);
        fishingRuning.SetActive(true);
        fishingStoped.SetActive(false);
        fishAnim.SetBool("isFishing", true);
        StartCoroutine("ShipRotate");
        Close();
    }
    public void StopFishing()
    {
        confirmStopFishing.SetActive(true);
        fishNumber.text = ship.GetCapacity().ToString();
        Debug.Log("Are You Suer Stop Fishing You will Get :" + ship.GetCapacity());
    }
    private void OnApplicationQuit()
    {
        if (fishAnim.GetBool("isFishing"))
        {
            PlayerPrefs.SetString(ship.GetShipName() + "Stop", "false");
            PlayerPrefs.SetString(ship.GetShipName() + "Fishing", "true");
            PlayerPrefs.SetFloat(ship.GetShipName() + "Xpos", transform.position.x);
            PlayerPrefs.SetFloat(ship.GetShipName() + "Ypos", transform.position.y);
        }
        else
        {
            PlayerPrefs.SetString(ship.GetShipName() + "Stop", "true");
            PlayerPrefs.SetString(ship.GetShipName() + "Fishing", "flase");
            PlayerPrefs.SetFloat(ship.GetShipName() + "Xpos", 0);
            PlayerPrefs.SetFloat(ship.GetShipName() + "Ypos", 0);
        }
    }
    //Save Date (Position , animations , active and not active Button ) On PlayerPrefs --
    private void OnApplicationPause(bool pause)
    {

        if (!pause)
        {
            OnBack?.Invoke(ship.GetShipName());
        }
        if (pause)
        {

            if (fishAnim.GetBool("isFishing"))
            {
                SetCurrentShipFishingOnPausedData();
            }
            else
            {
                SetCurrentShipNotFishingOnPausedData();
            }
        }
    }


    private void SetCurrentShipFishingOnPausedData()
    {
        ship.GetDataToJson().Fishing = "true";
        ship.GetDataToJson().Stop = "false";
        ship.GetDataToJson().Xpos = transform.position.x;
        ship.GetDataToJson().Ypos = transform.position.y;

        OnPaused?.Invoke(ship.GetDataToJson());
        //PlayerPrefs.SetString(ship.GetShipName() + "Stop", "false");
        //PlayerPrefs.SetString(ship.GetShipName() + "Fishing", "true");
        //PlayerPrefs.SetFloat(ship.GetShipName() + "Xpos", transform.position.x);
        //PlayerPrefs.SetFloat(ship.GetShipName() + "Ypos", transform.position.y);
    }

    private void SetCurrentShipNotFishingOnPausedData()
    {
        ship.GetDataToJson().Fishing = "false";
        ship.GetDataToJson().Stop = "true";
        ship.GetDataToJson().Xpos = 0;
        ship.GetDataToJson().Ypos = 0;

        OnPaused?.Invoke(ship.GetDataToJson());

        //PlayerPrefs.SetString(ship.GetShipName() + "Stop", "true");
        //PlayerPrefs.SetString(ship.GetShipName() + "Fishing", "flase");
        //PlayerPrefs.SetFloat(ship.GetShipName() + "Xpos", 0);
        //PlayerPrefs.SetFloat(ship.GetShipName() + "Ypos", 0);
    }

    /*private void OnApplicationPause(bool pause)
    {
        Debug.Log($"Pause = {pause} in ShipUIManaer.cs");
        if (!pause) return;
        if (fishAnim.GetBool("isFishing"))
        {
            
            PlayerPrefs.SetString(ship.GetShipName() + "Stop", "false");
            PlayerPrefs.SetString(ship.GetShipName() + "Fishing", "true");
            PlayerPrefs.SetFloat(ship.GetShipName() + "Xpos", transform.position.x);
            PlayerPrefs.SetFloat(ship.GetShipName() + "Ypos", transform.position.y);
        }
        else
        {
            PlayerPrefs.SetString(ship.GetShipName() + "Stop", "true");
            PlayerPrefs.SetString(ship.GetShipName() + "Fishing", "flase");
            PlayerPrefs.SetFloat(ship.GetShipName() + "Xpos", 0);
            PlayerPrefs.SetFloat(ship.GetShipName() + "Ypos", 0);
        }
    }*/
    /*private void OnApplicationQuit()
    {
        if (fishAnim.GetBool("isFishing"))
        {
            PlayerPrefs.SetString(ship.GetShipName() + "Stop", "false");
            PlayerPrefs.SetString(ship.GetShipName() + "Fishing", "true");
            PlayerPrefs.SetFloat(ship.GetShipName() + "Xpos", transform.position.x);
            PlayerPrefs.SetFloat(ship.GetShipName() + "Ypos", transform.position.y);
        }
        else
        {
            PlayerPrefs.SetString(ship.GetShipName() + "Stop", "true");
            PlayerPrefs.SetString(ship.GetShipName() + "Fishing", "flase");
            PlayerPrefs.SetFloat(ship.GetShipName() + "Xpos", 0);
            PlayerPrefs.SetFloat(ship.GetShipName() + "Ypos", 0);
        }
    }*/

    // we dont need any off this down methods for save i think 
    IEnumerator ShipRotate()
    {
        yield return new WaitForSeconds(2);
        LeanTween.scaleX(gameObject, 4, 0.4f);
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

    public void ConfirmStop()
    {
        confirmStopFishing.SetActive(false);
        LeanTween.moveX(gameObject, transform.position.x - 300, 2);
        fishingRuning.SetActive(false);
        fishingStoped.SetActive(true);
        fishAnim.SetBool("isFishing", false);
        PlayerPrefs.SetString(ship.GetShipName() + "Fishing", "false");
        Close();
    }

}
