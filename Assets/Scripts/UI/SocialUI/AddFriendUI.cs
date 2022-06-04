using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FishGame.UI.SocialUI
{
    public class AddFriendUI : MonoBehaviour
    {
        [SerializeField] string displayName;
        public static Action<string> OnAddFriend = delegate { };

        public void SetDisplayName(string name)
        {
            displayName = name;
        }

        public void AddFriend()
        {
            Debug.Log($"Add Friend Button clicked for {displayName}");
            if (string.IsNullOrEmpty(displayName)) return;
            OnAddFriend?.Invoke(displayName);
            Debug.Log("OnAddFriend Action is invoked");
        }
    }

}