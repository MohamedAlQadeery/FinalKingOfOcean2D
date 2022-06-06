using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace FishGame.UI.SocialUI
{
    public class MessagePopup : MonoBehaviour
    {
        [SerializeField] TMP_Text title;
        [SerializeField] TMP_Text messageBody;

        public void SetTitle(string msgtitle)
        {
            title.text = msgtitle;
        }

        public void SetMessageBody(string body)
        {
            messageBody.text = body;
        }


        public void OnClickDoneButton()
        {
            gameObject.SetActive(false);
        }
    }

}