using FishGame.Core;
using FishGame.Ships;
using FishGame.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FishGame.UI
{
    public class RegisterUI : MonoBehaviour
    {
        //Start Of register panel vars


        [SerializeField] TMP_InputField registerEmailInputField;
        [SerializeField] TMP_InputField registerPasswordInputField;
        [SerializeField] TMP_InputField registerUserNameInputField;
        [SerializeField] GameObject loginPanel;
        [SerializeField] GameObject giftRegister;
        [SerializeField] GameObject errorRegisterMessage;

        //End of register panel vars

        [SerializeField ]List<Ship> newPlayerShips;

        public void OnClickRegisterButton()
        {
            string email = registerEmailInputField.text;
            string password = registerPasswordInputField.text;
            string username = registerUserNameInputField.text;
            Debug.Log($"{email},{password},{username}");
            PlayFabAuth.Instance.RegisterWithEmail(email, password,username);
            SoundManager.Instance.PlaySound(SoundManager.Sound.ButtonSonud);
        }


        public void OnSuccessRegister(string message)
        {
            Debug.Log("From Register UI");
            Debug.Log(message);

            List<SerializableShipData> serializablesNewShips = ListUtil.Instance.ConvertToSerializableShipList(newPlayerShips);
            
            PlayFabPlayerData.Instance.NewPlayerSetup(serializablesNewShips);


            giftRegister.SetActive(true);

        }

        public void GiftRegister()
        {
            SceneManager.LoadScene(1);
        }

      

        public void OnErrorRegister(string message)
        {
            GameObject errorLogin = Instantiate(errorRegisterMessage, transform.position, transform.rotation) as GameObject;
            errorLogin.transform.SetParent(GameObject.FindGameObjectWithTag("UILogin").transform, false);
            Destroy(errorLogin, 2);
            SoundManager.Instance.PlaySound(SoundManager.Sound.ErrorSound);
            Debug.Log(message);
        }

        public void Close()
        {
            SoundManager.Instance.PlaySound(SoundManager.Sound.CloseSonud);
            Destroy(gameObject);
        }

        public void LoginPanel()
        {
            SoundManager.Instance.PlaySound(SoundManager.Sound.ButtonSonud);
            Close();
            GameObject newUiSginUp = Instantiate(loginPanel, transform.position, transform.rotation) as GameObject;
            newUiSginUp.transform.SetParent(GameObject.FindGameObjectWithTag("UILogin").transform, false);
        }
    }

}