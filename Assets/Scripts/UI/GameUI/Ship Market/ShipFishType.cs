using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace FishGame.UI.GameUI.ShipMarketUI
{
    public class ShipFishType : MonoBehaviour
    {
        [SerializeField] Image fishIcon;
        [SerializeField] TMP_Text fishName;
    

        public void SetFishType(Sprite icon,string name)
        {
            fishIcon.sprite = icon;
            fishName.text = name;
        }
    
    }

}