using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using FishGame.Core;

namespace FishGame.UI.SocialUI
{
    public class FriendListBox : MonoBehaviour
    {
        [SerializeField] TMP_Text friendName;
        [SerializeField] TMP_Text friendLevel;
        private string playFabId;
        //when clicking on the name
        public static Action<string> OnShowProfile = delegate { };
        public static Action<string> OnRemoveFriend = delegate { };

       
        public void SetPlayFabId(string id)
        {
            playFabId = id;
        }
        public void SetFriendName(string name)
        {
            friendName.text = name;
        }

        public void SetFriendLevel(string level)
        {
            friendLevel.text = level;
        }

        public void OnClickShowProfileIcon()
        {
            OnShowProfile?.Invoke(playFabId);
        }

       public void OnClickOnRemoveFriend()
        {
            OnRemoveFriend?.Invoke(playFabId);
        }


    }

}