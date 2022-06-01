using PlayFab;
using PlayFab.ClientModels;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace FishGame.Core
{
    enum FriendIdType { PlayFabId, Username, Email, DisplayName };

    public class PlayFabFriendsListEvent: UnityEvent<List<FriendInfo>> { }

    public class PlayFabSocial : MonoBehaviour
    {
        public static List<FriendInfo> friendsList = null;
        public PlayFabFriendsListEvent onGetFriendListSuccess;
        public PlayFabEvent onAddFriendSuccess;
        public PlayFabEvent onPlayFabSocialError;
       public void GetFriendsList()
        {
            var request = new GetFriendsListRequest
            {
                IncludeSteamFriends = false,
                IncludeFacebookFriends = false,
                XboxToken = null
            };

            PlayFabClientAPI.GetFriendsList(request,result => {
                friendsList = result.Friends;
                onGetFriendListSuccess?.Invoke(friendsList);

            },error => {

                Debug.Log($"Error in getting friend list : +{error.ErrorMessage} ");

            });
        }



        void AddFriend(FriendIdType idType, string friendId)
        {
            var request = new AddFriendRequest();
            switch (idType)
            {
                case FriendIdType.PlayFabId:
                    request.FriendPlayFabId = friendId;
                    break;
                case FriendIdType.Username:
                    request.FriendUsername = friendId;
                    break;
                case FriendIdType.Email:
                    request.FriendEmail = friendId;
                    break;
                case FriendIdType.DisplayName:
                    request.FriendTitleDisplayName = friendId;
                    break;
            }
            // Execute request and update friends when we are done
            PlayFabClientAPI.AddFriend(request, result => {
                onAddFriendSuccess?.Invoke("Friends added successfully");
            }, error=> {
                Debug.Log($"Error in Adding friend : +{error.ErrorMessage} ");

            });
        }

        private void OnPlayFabSocialError(PlayFabError obj)
        {
            onPlayFabSocialError?.Invoke(obj.ErrorMessage);
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