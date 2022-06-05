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
        [SerializeField] FriendListBox friendListBoxPrefab;
        [SerializeField] Transform friendListContent;

        private void Awake()
        {
            displayedFriends = new List<FriendInfo>();
            PlayFabSocial.OnFriendListUpdated += HandleOnFriendListUpdated;
        }

        private void OnDestroy()
        {
            PlayFabSocial.OnFriendListUpdated -= HandleOnFriendListUpdated;

        }

        private void HandleOnFriendListUpdated(List<FriendInfo> list)
        {
            ClearCurrentFriendList(list);

            foreach (var friend in displayedFriends)
            {
                FillFriendListBox(friend);
            }
        }

        private void ClearCurrentFriendList(List<FriendInfo> list)
        {
            displayedFriends.Clear();
            displayedFriends = list;
            foreach (Transform transform in friendListContent)
            {
                Destroy(transform.gameObject);
            }
        }

        private void FillFriendListBox(FriendInfo friend)
        {
            FriendListBox friendListBox = Instantiate(friendListBoxPrefab,friendListContent);
            friendListBox.SetFriendName(friend.Username);
        }

        private void Start()
        {
            OnGetFriends?.Invoke();  
        }




    }

}