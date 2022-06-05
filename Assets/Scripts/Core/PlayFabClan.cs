using FishGame.UI.SocialUI;
using PlayFab;
using PlayFab.GroupsModels;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FishGame.Core
{
    public class PlayFabClan : MonoBehaviour
    {
        public static Action<ListMembershipResponse> OnClanListUpdated = delegate { };
        private  string entityId;
        private  string entityType;

        private void Awake()
        {
            
            entityId = PlayFabAuth.GetEntityId();
            entityType = PlayFabAuth.GetEntityType();
            ClanManagmentUI.OnCreateClan += HandleCreateClan;
            ClanManagmentUI.OnGetClans += GetClanList;
        }

        private void OnDestroy()
        {
            ClanManagmentUI.OnCreateClan -= HandleCreateClan;
            ClanManagmentUI.OnGetClans -= GetClanList;

        }
        private void HandleCreateClan(string clanName)
        {
            var request = new CreateGroupRequest { GroupName = clanName };
            PlayFabGroupsAPI.CreateGroup(request,OnCreateClanResponse,OnCreateClanError);
        }

        private void OnCreateClanResponse(CreateGroupResponse obj)
        {
            Debug.Log("Clan created successfully");
            GetClanList();
        }

        private void OnCreateClanError(PlayFabError obj)
        {
            Debug.Log($"Error : {obj.GenerateErrorReport()}");

        }

        public void GetClanList()
        {
            PlayFabGroupsAPI.ListMembership(new ListMembershipRequest(),OnGetCanList,OnGetClanListError);

        }

        private void OnGetCanList(ListMembershipResponse clanList)
        {
            OnClanListUpdated?.Invoke(clanList);
        }

        private void OnGetClanListError(PlayFabError obj)
        {
            Debug.Log($"Error : {obj.GenerateErrorReport()}");
        }

        

    }

}