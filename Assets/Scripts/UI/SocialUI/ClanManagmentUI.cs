using FishGame.Core;
using PlayFab.GroupsModels;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace FishGame.UI.SocialUI
{
    public class ClanManagmentUI : MonoBehaviour
    {
        [SerializeField] private string clanName;
        [SerializeField] private TMP_Text joinedClanText;
        public static Action<string> OnCreateClan = delegate { };
        public static Action OnGetClans = delegate { };
        

        private void Awake()
        {
            PlayFabClan.OnClanListUpdated += HandleClanListUpdated;
        }

        private void HandleClanListUpdated(ListMembershipResponse obj)
        {
            foreach (var pair in obj.Groups)
            {
                joinedClanText.text += $"Clan Id : {pair.Group.Id} and Clan Name : {pair.GroupName}";
            }
        }

        private void OnDestroy()
        {
            PlayFabClan.OnClanListUpdated -= HandleClanListUpdated;

        }
        public void SetClanName(string name)
        {
            clanName = name;
        }

        public void CreateClan()
        {
            Debug.Log($"Create Clan Button clicked for {clanName}");
            if (string.IsNullOrEmpty(clanName)) return;
            OnCreateClan?.Invoke(clanName);
            Debug.Log($"Create Clan Button clicked for {clanName}");

        }

        private void Start()
        {
           // OnGetClans?.Invoke();
        }

    }

}