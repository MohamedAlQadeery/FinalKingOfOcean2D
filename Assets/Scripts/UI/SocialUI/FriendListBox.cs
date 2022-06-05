using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace FishGame.UI.SocialUI
{
    public class FriendListBox : MonoBehaviour
    {
        [SerializeField] TMP_Text friendName;
        [SerializeField] TMP_Text friendLevel;

        public void SetFriendName(string name)
        {
            friendName.text = name;
        }

        public void SetFriendLevel(string level)
        {
            friendLevel.text = level;
        }


    }

}