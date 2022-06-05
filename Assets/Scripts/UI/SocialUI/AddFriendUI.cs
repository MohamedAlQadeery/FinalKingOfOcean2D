using FishGame.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FishGame.UI.SocialUI
{
    public class AddFriendUI : MonoBehaviour
    {
        [SerializeField] string displayName;
        [SerializeField] MessagePopup messagePrefab;

        public static Action<string> OnAddFriend = delegate { };

        private void Awake()
        {
            PlayFabSocial.OnGetResponceMessage += HandleOnGetResponceMessage;
        }
        private void OnDestroy()
        {
            PlayFabSocial.OnGetResponceMessage -= HandleOnGetResponceMessage;

        }
        private void HandleOnGetResponceMessage(string title, string body)
        {
            messagePrefab.SetTitle(title);
            messagePrefab.SetMessageBody(body);
            messagePrefab.gameObject.SetActive(true);
        }

       
        public void SetDisplayName(string name)
        {
            displayName = name;
        }

        public void AddFriend()
        {
            Debug.Log($"Add Friend Button clicked for {displayName}");
            if (string.IsNullOrEmpty(displayName)) return;
            OnAddFriend?.Invoke(displayName);
            gameObject.SetActive(false);
            Debug.Log("OnAddFriend Action is invoked");
        }

        public void OnClickCancelButton()
        {
            gameObject.SetActive(false);
        }
    }

}