using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TimeQuitTest : MonoBehaviour
{
    [SerializeField] TMP_Text textCount;
    int count = 1;
    DateTime quitDateTime;
    // Start is called before the first frame update
    void Start()
    {
        string dateQuitString = PlayerPrefs.GetString("quitDateTime", "");
        quitDateTime = DateTime.Parse(dateQuitString);
        Debug.Log("App Start");
        Debug.Log("Time Start : " + (int)(DateTime.Now - quitDateTime).TotalSeconds);
        Debug.Log("Time Start1 : " + quitDateTime.Second);
        //Debug.Log("Test !> : " + (int)(DateTime.Now - quitDateTime).TotalSeconds);
        if (!dateQuitString.Equals("")) {
            StartCoroutine("Count");
        }
    }

    IEnumerator Count()
    {
        while (true)
        {
            count = (int)(DateTime.Now - quitDateTime).TotalSeconds;
            textCount.text = (count++).ToString();
            yield return new WaitForSeconds(1f);
        }
    }
    private void OnApplicationPause(bool pause)
    {
    }

    private void OnApplicationQuit()
    {
       // Debug.Log("Time Quit : " + quitDateTime.Second);
       // Debug.Log("Time Quit : " + (int)(DateTime.Now - quitDateTime).TotalSeconds);
        DateTime quitDate= DateTime.Now;        
        PlayerPrefs.SetString("quitDateTime", quitDate.ToString());
       // Debug.Log("App Quit1");
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void ButtonClick()
    {
        SoundManager.Instance.PlaySound(SoundManager.Sound.ButtonSonud);
        Debug.Log("Time now :" + (int)(DateTime.Now - quitDateTime).TotalSeconds);
        
    }
}
