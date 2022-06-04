
using UnityEngine;
using PlayFab.ClientModels;
using PlayFab;
using System;
using UnityEngine.Events;
using Facebook.Unity;
using System.Collections.Generic;

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
        private static string entityId;
        private static string entityType;
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


        private void Start()
        {
            Debug.Log(FB.IsInitialized);
           // if (FB.IsInitialized) return;
           //there is issuse i'll solve it later
            //FB.Init(OnFacebookInitialized);


        }
      

        public void OnFacebookInitialized()
        {
            // Once Facebook SDK is initialized, if we are logged in, we log out to demonstrate the entire authentication cycle.
            if (FB.IsLoggedIn)
                FB.LogOut();

          
        }


        public void LoginWithFacebook()
        {
            // We invoke basic login procedure and pass in the callback to process the result
            FB.LogInWithReadPermissions(new List<string> { "public_profile", "email" }, result => {

                // If result has no errors, it means we have authenticated in Facebook successfully
                if (result == null || string.IsNullOrEmpty(result.Error))
                {
                    Debug.Log("Facebook Auth Complete! Access Token: " + AccessToken.CurrentAccessToken.TokenString + "\nLogging into PlayFab...");

                    /*
                     * We proceed with making a call to PlayFab API. We pass in current Facebook AccessToken and let it create
                     * and account using CreateAccount flag set to true. We also pass the callback for Success and Failure results
                     */
                    PlayFabClientAPI.LoginWithFacebook(new LoginWithFacebookRequest { CreateAccount = true, AccessToken = AccessToken.CurrentAccessToken.TokenString },
                        OnPlayfabFacebookAuthComplete, OnPlayfabFacebookAuthFailed);
                }
                else
                {
                    // If Facebook authentication failed, we stop the cycle with the message
                    Debug.Log("Facebook Auth Failed: " + result.Error + "\n" + result.RawResult);
                }
            });
        }

      
        private void OnPlayfabFacebookAuthFailed(PlayFabError error)
        {
            Debug.LogError("PlayFab Facebook Auth Failed: " + error.GenerateErrorReport());
        }

        private void OnPlayfabFacebookAuthComplete(PlayFab.ClientModels.LoginResult result)
        {
            Debug.Log("PlayFab Facebook Auth Complete. Session ticket: " + result.SessionTicket);
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

    

        private void OnLoginSuccess(PlayFab.ClientModels.LoginResult result)
        {
            if (RememberMe)
            {
                AssignCustomLink();
            }
            entityId = result.EntityToken.Entity.Id;
            // The expected entity type is title_player_account.
            entityType = result.EntityToken.Entity.Type;

            Debug.Log($"Entity id = {entityId}");
            Debug.Log($"Entity type = {entityType}");
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


        #region Getters

       
        public static string GetPlayerFabId()
        {
            return playerFabId;
        }


        public static string GetGameTitle()
        {
            return gameTitle;
        }


        public static string GetEntityId()
        {
            return entityId;
        }

        public static string GetEntityType()
        {
            return entityType;
        }
        #endregion
    }
}
