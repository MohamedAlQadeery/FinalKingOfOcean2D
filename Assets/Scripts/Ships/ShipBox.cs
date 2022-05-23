using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace FishGame.Ships
{
    public class ShipBox : MonoBehaviour
    {
        [SerializeField] Image Image;
        [SerializeField] TMP_Text shipName;
        [SerializeField] TMP_Text shipPrice;

        public void OnClickShipInfo()
        {
            Debug.Log("Ship info displayed here");
        }

        public void SetShipBoxName(string name)
        {
            shipName.text = name;
        }

        public void SetShipBoxPrice(string price)
        {
            shipPrice.text = price;
        }

        public void SetShipImage(Sprite shipImage)
        {
            Image.sprite = shipImage;
        }

    }

}