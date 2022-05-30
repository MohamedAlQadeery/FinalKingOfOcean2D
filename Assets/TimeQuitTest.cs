using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TimeQuitTest : MonoBehaviour
{
    [SerializeField] TMP_Text textCount;
    public int count = 1;
    DateTime quitDateTime1;
    string dateQuitString;
    DateTime quitDateTime;
    int timeLeft;
    // Start is called before the first frame update
    void Start()
    {
        
        string dateQuitString = PlayerPrefs.GetString("quitDateTime", "");
        string countString = PlayerPrefs.GetString("Count", "");
        count = int.Parse(countString);       
        quitDateTime = DateTime.Parse(dateQuitString);       
        timeLeft = ((int)(DateTime.Now - quitDateTime).TotalSeconds) - 43200;
        count = count + timeLeft;

        Debug.Log(((int)(DateTime.Now - quitDateTime).TotalSeconds) - 43200);
        Debug.Log(countString);

        if (!dateQuitString.Equals("")) {
            StartCoroutine("Count");
        }
    }
    IEnumerator Count()
    {
        while(true)
        {
            textCount.text = (count++).ToString();
            yield return new WaitForSeconds(1f);
        }         
            
        
    }
    private void OnApplicationQuit()
    {
        if (count > 10)
        {
            count = 1;
            PlayerPrefs.SetString("quitDateTime", "");
        }
        DateTime quitDate= DateTime.Now;        
        PlayerPrefs.SetString("quitDateTime", quitDate.ToString());
        PlayerPrefs.SetString("Count",count.ToString());
        
    }


    public void ClearPlayPrefs()
    {
        
    }

}
