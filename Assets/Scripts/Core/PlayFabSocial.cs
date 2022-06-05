using FishGame.UI.SocialUI;
using PlayFab;
using PlayFab.ClientModels;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace FishGame.Core
{


    public class PlayFabSocial : MonoBehaviour
    {

       private List<FriendInfo> friendsList ;
        public static Action<List<FriendInfo>> OnFriendListUpdated = delegate { };

        private void Awake()
        {
            friendsList = new List<FriendInfo>();
            AddFriendUI.OnAddFriend += HandleAddFriend;
            DisplayFriendsUI.OnGetFriends+=GetFriends;
        }

        private void GetFriends()
        {
            HandleGetFriendsList();
        }

        private void OnDestroy()
        {
            AddFriendUI.OnAddFriend -= HandleAddFriend;
            DisplayFriendsUI.OnGetFriends -= GetFriends;


        }


        public void HandleGetFriendsList()
        {
            Debug.Log("Playfab is getting friend list..");
            var request = new GetFriendsListRequest
            {
                IncludeSteamFriends = false,
                IncludeFacebookFriends = false,
                XboxToken = null
            };

            PlayFabClientAPI.GetFriendsList(request,result => {
                Debug.Log($"Playfab get friend list success: {result.Friends.Count}");
                friendsList = result.Friends;
                OnFriendListUpdated?.Invoke(result.Friends);

            },error => {

                Debug.Log($"Error in getting friend list : +{error.ErrorMessage} ");

            });
        }



        void HandleAddFriend(string name)
        {
            var request = new AddFriendRequest { FriendUsername= name};
            
            // Execute request and update friends when we are done
            PlayFabClientAPI.AddFriend(request, result => {
                Debug.Log($"{name} is added to your friend list successfully");
                HandleGetFriendsList();
            }, error=> {
                Debug.Log($"Error in Adding friend : +{error.ErrorMessage} ");

            });
        }

        private void OnPlayFabSocialError(PlayFabError obj)
        {
            Debug.LogError(obj.GenerateErrorReport());
        }


        // unlike AddFriend, RemoveFriend only takes a PlayFab ID
        // you can get this from the FriendInfo object under FriendPlayFabId
        void RemoveFriend(FriendInfo friendInfo)
        {
            PlayFabClientAPI.RemoveFriend(new RemoveFriendRequest
            {
                FriendPlayFabId = friendInfo.FriendPlayFabId
            }, result => {
                friendsList.Remove(friendInfo);
            }, error=> {
                Debug.Log($"Error in removing friend : +{error.ErrorMessage} ");
            });
        }
    }

}