using FishGame.Core;
using FishGame.Fishes;
using FishGame.Ships;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipFishing : MonoBehaviour
{
    [SerializeField] Ship currentShip;
    [SerializeField] float timeToFillCapacity;
    [SerializeField] bool startFishing = false;
    [SerializeField] Button completeButton;

    public float TimeToFishing()
    {
        return currentShip.GetFishingDuration();
    }

}
