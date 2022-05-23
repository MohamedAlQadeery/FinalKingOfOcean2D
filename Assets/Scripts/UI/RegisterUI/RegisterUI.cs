using FishGame.Core;
using FishGame.Ships;
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





        //End of register panel vars

        [SerializeField ]List<Ship> newPlayerShips;

        public void OnClickRegisterButton()
        {
            string email = registerEmailInputField.text;
            string password = registerPasswordInputField.text;
            string username = registerUserNameInputField.text;
            Debug.Log($"{email},{password},{username}");
            PlayFabAuth.Instance.RegisterWithEmail(email, password,username);
        }


        public void OnSuccessRegister(string message)
        {
            Debug.Log("From Register UI");
            Debug.Log(message);

            List<SerializableShipData> serializablesNewShips = FillSerializableShipList();
            
            PlayFabPlayerData.Instance.NewPlayerSetup(serializablesNewShips);


            giftRegister.SetActive(true);

        }

        public void GiftRegister()
        {
            SceneManager.LoadScene(1);
        }

        private List<SerializableShipData> FillSerializableShipList()
        {
            List<SerializableShipData> tmpList = new List<SerializableShipData>();
            foreach(Ship ship in newPlayerShips)
            {
                tmpList.Add(ship.GetDataToJson());
            }

            return tmpList;
        }

        public void OnErrorRegister(string message)
        {
            Debug.Log(message);
        }

        public void Close()
        {
            Destroy(gameObject);
        }

        public void LoginPanel()
        {
            Close();
            GameObject newUiSginUp = Instantiate(loginPanel, transform.position, transform.rotation) as GameObject;
            newUiSginUp.transform.SetParent(GameObject.FindGameObjectWithTag("UILogin").transform, false);
        }
    }

}