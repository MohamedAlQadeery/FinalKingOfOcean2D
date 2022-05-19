using FishGame.Core;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

namespace FishGame.UI
{
    public class LoginUI : MonoBehaviour
    {
        //Start Of login panel vars

        [SerializeField] TMP_InputField loginEmailInputField;
        [SerializeField] TMP_InputField loginPasswordInputField;
        [SerializeField] Toggle loginRememberMe;
        [SerializeField] GameObject signUpPanel;
        [SerializeField] GameObject signOutButton;

        //End of login panel vars
        PlayFabAuth playFabAuthService ;
        private void Awake()
        {
            playFabAuthService = PlayFabAuth.Instance;
            //Set our remember me button to our remembered state.
            loginRememberMe.isOn = playFabAuthService.RememberMe;

            //Subscribe to our Remember Me toggle
            loginRememberMe.onValueChanged.AddListener((toggle) =>
            {
                playFabAuthService.RememberMe = toggle;
            });

        
            playFabAuthService.CheckCustomLink();

        }


        public void OnClickSignOutButton()
        {
            playFabAuthService.ClearAuth();
            playFabAuthService.ClearRememberMe();
        }
        public void OnClickLoginButton()
        {
            string email = loginEmailInputField.text;
            string password = loginPasswordInputField.text;
            playFabAuthService.LoginWithEmail(email, password);
           
        }

        public void OnLoginSuccess(string message)
        {
            Debug.Log("Message from login UI");
            Debug.Log(message+"?!");
            

         

           SceneManager.LoadScene(1);

        }


        public void OnLoginError(string message)
        {
            Debug.Log(message);
        }

        public void Close()
        {
            Destroy(gameObject);
        }

        public void SignUpPanel()
        {
            Close();
            GameObject newUiSginUp = Instantiate(signUpPanel, transform.position, transform.rotation) as GameObject;
            newUiSginUp.transform.SetParent(GameObject.FindGameObjectWithTag("UILogin").transform, false);
        }


        private void Update()
        {
            
        }
    }

}