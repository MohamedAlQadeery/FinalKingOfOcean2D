using PlayFab.ClientModels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using FishGame.Core;

namespace FishGame.UI.SocialUI
{
    public class DisplayFriendsUI : MonoBehaviour
    {
        public static Action OnGetFriends = delegate { };
        private List<FriendInfo> displayedFriends;
        [SerializeField]private TMP_Text friendsText;


        private void Awake()
        {
            displayedFriends = new List<FriendInfo>();
            PlayFabSocial.OnFriendListUpdated += HandleOnFriendListUpdated;
        }

        private void OnDestroy()
        {
            PlayFabSocial.OnFriendListUpdated -= HandleOnFriendListUpdated;

        }

        private void HandleOnFriendListUpdated(List<FriendInfo> obj)
        {
            displayedFriends = obj;
            friendsText.text = string.Empty;
            foreach (var friend in displayedFriends)
            {
                friendsText.text+= $"Name : {friend.Username},";
            }
        }

        private void Start()
        {
            OnGetFriends?.Invoke();  
        }




    }

}