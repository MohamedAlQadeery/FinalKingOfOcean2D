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
        //[SerializeField] GameObject signOutButton;
        [SerializeField] GameObject loadingGamePanel;
        [SerializeField] GameObject errorLoginMessage;
        //End of login panel vars
        PlayFabAuth playFabAuthService ;
        [SerializeField] Button loginButton;
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
            SoundManager.Instance.PlaySound(SoundManager.Sound.ButtonSonud);
            playFabAuthService.ClearAuth();
            playFabAuthService.ClearRememberMe();
        }
        public void OnClickLoginButton()
        {           
            string email = loginEmailInputField.text;
            string password = loginPasswordInputField.text;
            playFabAuthService.LoginWithEmail(email, password);
            loginButton.gameObject.SetActive(false);
        }

        public void OnLoginSuccess(string message)
        {
            SoundManager.Instance.PlaySound(SoundManager.Sound.ButtonSonud);
            Debug.Log("Message from login UI");
            Debug.Log(message+"?!");




            StartCoroutine(LoadingGame());
            loginButton.gameObject.SetActive(true);
        }

        IEnumerator LoadingGame()
        {

            loadingGamePanel.SetActive(true);
            yield return new WaitForSeconds(2);
            SceneManager.LoadSceneAsync(1);

            // SceneManager.LoadScene(2); // mohamed game scene
        }


        public void OnLoginError(string message)
        {
            GameObject errorLogin = Instantiate(errorLoginMessage, transform.position, transform.rotation) as GameObject;
            errorLogin.transform.SetParent(GameObject.FindGameObjectWithTag("UILogin").transform, false);
            SoundManager.Instance.PlaySound(SoundManager.Sound.ErrorSound);
            Destroy(errorLogin,2);
            Debug.Log(message);
            loginButton.gameObject.SetActive(true);

        }

        public void Close()
        {
            SoundManager.Instance.PlaySound(SoundManager.Sound.CloseSonud);
            Destroy(gameObject);
        }

        public void SignUpPanel()
        {
            SoundManager.Instance.PlaySound(SoundManager.Sound.ButtonSonud);
            Close();
            GameObject newUiSginUp = Instantiate(signUpPanel, transform.position, transform.rotation) as GameObject;
            newUiSginUp.transform.SetParent(GameObject.FindGameObjectWithTag("UILogin").transform, false);
        }


        private void Update()
        {
            
        }
    }

}