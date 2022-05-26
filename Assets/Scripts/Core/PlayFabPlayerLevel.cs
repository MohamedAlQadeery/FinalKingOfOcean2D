
using System;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System.Collections.Generic;

namespace FishGame.Core
{
    public class PlayFabPlayerLevel : MonoBehaviour
    {
        private static PlayFabPlayerLevel _instance;

        public static PlayFabPlayerLevel Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new PlayFabPlayerLevel();
                }
                return _instance;
            }

        }

        public PlayFabPlayerLevel()
        {
            _instance = this;
         
        }

        LevelSystem levelSystem;
        private const string levelKey = "level";
        private const string expKey = "experince";
        private void Awake()
        {
            levelSystem = LevelSystem.Instance;
            levelSystem.OnExperinceGained.AddListener(OnUserExperinceGained);
            levelSystem.OnLevelChanged.AddListener(OnUserLevelChanged);
        }

        private void OnDisable()
        {
            levelSystem.OnExperinceGained.RemoveListener(OnUserExperinceGained);
            levelSystem.OnLevelChanged.RemoveListener(OnUserLevelChanged);
        }

        private void OnUserLevelChanged()
        {
            var request = new UpdateUserDataRequest
            {
                Data= new Dictionary<string, string>() {
                    {levelKey,levelSystem.GetCurrentLevel().ToString() }
                },
            };

            PlayFabClientAPI.UpdateUserData(request, null, null);
        }

        private void OnUserExperinceGained()
        {
            var request = new UpdateUserDataRequest
            {
                Data = new Dictionary<string, string>() {
                    {expKey,levelSystem.GetCurrentExperince().ToString() }
                },
            };
            PlayFabClientAPI.UpdateUserData(request, null, null);

        }
    }

}