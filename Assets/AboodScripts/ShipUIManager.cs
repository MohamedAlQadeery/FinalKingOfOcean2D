using FishGame.Ships;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipUIManager : MonoBehaviour
{

    public Animator fishAnim;
    public GameObject shipPopUpPanel;
    public GameObject fishingRuning;
    public GameObject fishingStoped;
    public GameObject infoPanel;
    public Ship ship;

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
        if (PlayerPrefs.GetFloat(ship.GetShipName()+"Xpos") != 0) {
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
        LeanTween.moveX(gameObject, transform.position.x - 300, 2);
        fishingRuning.SetActive(false);
        fishingStoped.SetActive(true);
        fishAnim.SetBool("isFishing", false);
        Close();
    }

    //Save Date (Position , animations , active and not active Button ) On PlayerPrefs --
    private void OnApplicationQuit()
    {
        if (fishAnim.GetBool("isFishing"))
        {
            PlayerPrefs.SetString(ship.GetShipName() + "Stop", "false");
            PlayerPrefs.SetString(ship.GetShipName() + "Fishing", "true");
            PlayerPrefs.SetFloat(ship.GetShipName() + "Xpos",transform.position.x);
            PlayerPrefs.SetFloat(ship.GetShipName() + "Ypos",transform.position.y);
        }
        else
        {
            PlayerPrefs.SetString(ship.GetShipName() + "Stop", "true");
            PlayerPrefs.SetString(ship.GetShipName() + "Fishing", "flase");
            PlayerPrefs.SetFloat(ship.GetShipName() + "Xpos", 0);
            PlayerPrefs.SetFloat(ship.GetShipName() + "Ypos", 0);
        }
    }

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
}
