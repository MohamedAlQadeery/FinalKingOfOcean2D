
using System;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System.Collections.Generic;
using UnityEngine.Events;
using FishGame.Systems;

namespace FishGame.Core
{
    [Serializable]
    public class PlayFabPlayerProgressionEvent : UnityEvent<int,int> { }


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

        LevelSystem levelSystem ;
        private const string levelKey = "level";
        private const string expKey = "experince";


      public UnityEvent OnLeveLUpdatedSuccess;
       public UnityEvent OnExpUpdatedSuccess;
       public PlayFabPlayerProgressionEvent OnGetLevelAndExpSuccess;


        private void Awake()
        {
            levelSystem = LevelSystem.Instance;
        }

        private void OnEnable()
        {
         
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
            Debug.LogError("Inside OnUserLevelChanged()");
            var request = new UpdateUserDataRequest
            {
                Data= new Dictionary<string, string>() {
                    {levelKey,levelSystem.GetCurrentLevel().ToString() }
                },
            };

            PlayFabClientAPI.UpdateUserData(request, result=> {
                OnLeveLUpdatedSuccess?.Invoke();
               
            
            }, null);
        }

        private void OnUserExperinceGained()
        {
            Debug.LogError("Inside OnUserExperinceGained()");

            var request = new UpdateUserDataRequest
            {
                Data = new Dictionary<string, string>() {
                    {expKey,levelSystem.GetCurrentExperince().ToString() }
                },
            };
            PlayFabClientAPI.UpdateUserData(request, result => {
                OnExpUpdatedSuccess?.Invoke();
            
            }, null);

        }

        public void GetUserCurrentLevelAndExp()
        {
            var request = new GetUserDataRequest
            {
                Keys = new List<string> { levelKey,expKey },
            };

            PlayFabClientAPI.GetUserData(request, result => {

                OnGetLevelAndExpSuccess?.Invoke(int.Parse(result.Data[levelKey].Value),int.Parse(result.Data[expKey].Value));


            }, null);

        }


    }

}