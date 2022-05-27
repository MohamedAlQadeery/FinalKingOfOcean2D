using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayFabMarket : MonoBehaviour
{
    private PlayFabMarket _instance;
    public PlayFabMarket Instance
    {
        get { 
        if(_instance == null)
            {
                _instance = new PlayFabMarket();
            }

            return _instance;
        }
    }

    public PlayFabMarket()
    {
        _instance = this;
    }
    



}
