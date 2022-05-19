
using UnityEngine;
using PlayFab.ClientModels;
using PlayFab;
using System;
using UnityEngine.Events;

namespace FishGame.Core
{

    [Serializable]
    public class PlayFabEvent : UnityEvent<string>
    {

    }
    public class PlayFabAuth : MonoBehaviour
    {
        private static string playerFabId ;
        private static string gameTitle = "EEF6F";

        [SerializeField] PlayFabEvent loginSuccessEvent;
        [SerializeField] PlayFabEvent errorEvent;
        [SerializeField] PlayFabEvent registerSuccessEvent;

        private const string _loginRememberKey = "PlayFabLoginRemember";
        private const string _playFabRememberMeIdKey = "PlayFabIdPassGuid";
        public bool RememberMe
        {
            get
            {
                return PlayerPrefs.GetInt(_loginRememberKey, 0) == 0 ? false : true;
            }
            set
            {
                PlayerPrefs.SetInt(_loginRememberKey, value ? 1 : 0);
            }
        }


        public static PlayFabAuth Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new PlayFabAuth();
                }
                return _instance;
            }
        }
        private static PlayFabAuth _instance;

        public PlayFabAuth()
        {
            _instance = this;
        }

        public string RememberMeId
        {
            get
            {
                return PlayerPrefs.GetString(_playFabRememberMeIdKey, "");
            }
            set
            {
                var duid = string.IsNullOrEmpty(value) ? SystemInfo.deviceUniqueIdentifier : value;
                PlayerPrefs.SetString(_playFabRememberMeIdKey, duid);
            }
        }


        public void RegisterWithEmail(string email,string password,string username)
        {
            var request = new RegisterPlayFabUserRequest {
                Email = email,
                Password = password,
                Username=username,
                TitleId = gameTitle,

            };

            PlayFabClientAPI.RegisterPlayFabUser(request, OnSuccessRegister, OnError);
        }


        

        public void LoginWithEmail(string email, string password)
        {
           
            var request = new LoginWithEmailAddressRequest
            {
                Email = email,
                Password = password,
                TitleId = gameTitle,
                
            };

            PlayFabClientAPI.LoginWithEmailAddress(request,OnLoginSuccess,OnError);
        }

        private void OnLoginSuccess(LoginResult result)
        {
            if (RememberMe)
            {
                AssignCustomLink();
            }
            loginSuccessEvent?.Invoke("Login Successfully");

        }


      

        private void OnError(PlayFabError error)
        {
            errorEvent?.Invoke($"Error : {error.GenerateErrorReport()}");
        }

        private void OnSuccessRegister(RegisterPlayFabUserResult result)
        {

            registerSuccessEvent?.Invoke("Registerd successfully and logged in");
          

        }




        public void CheckCustomLink()
        {
            if (RememberMe && !string.IsNullOrEmpty(RememberMeId))
            {
                LoginUsingCustomId();
                return;
            }


        }

        private void LoginUsingCustomId()
        {
            var customLoginRequest = new LoginWithCustomIDRequest
            {
                CustomId = RememberMeId,

            };

            PlayFabClientAPI.LoginWithCustomID(customLoginRequest, OnLoginSuccess, OnError);
        }


        /*
        if remeberMe is checked we link custom id to the user 
        so we can login again using the customId
        */

        public void AssignCustomLink()
        {

            RememberMeId = SystemInfo.deviceUniqueIdentifier;
            var request = new LinkCustomIDRequest
                {
                    CustomId = RememberMeId,
                    ForceLink = false // If another user is already linked to the custom ID, unlink the other user and re-link.
                };
                PlayFabClientAPI.LinkCustomID(request, null, null);
            

        }


        public void ClearRememberMe()
        {
            PlayerPrefs.DeleteKey(_loginRememberKey);
            PlayerPrefs.DeleteKey(_playFabRememberMeIdKey);
        }

        public void ClearAuth()
        {
            PlayFabClientAPI.ForgetAllCredentials();

        }


        public static string GetPlayerFabId()
        {
            return playerFabId;
        }


        public static string GetGameTitle()
        {
            return gameTitle;
        }
    }
}
