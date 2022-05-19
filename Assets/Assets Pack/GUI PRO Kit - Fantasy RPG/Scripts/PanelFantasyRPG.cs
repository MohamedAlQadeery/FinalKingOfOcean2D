using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
namespace LayerLab.FantasyRPG
{
    public class PanelFantasyRPG : MonoBehaviour
    {
        [SerializeField] private GameObject[] otherPanels;
        [SerializeField] private GameObject signUp;
        public void OnEnable()
        {
            for (int i = 0; i < otherPanels.Length; i++) otherPanels[i].SetActive(true);
        }

        public void OnDisable()
        {
            for (int i = 0; i < otherPanels.Length; i++) otherPanels[i].SetActive(false);
        }

        public void Close()
        {
            Destroy(gameObject);
        }

        public void SignUp()
        {
            Close();
            GameObject newUiSginUp = Instantiate(signUp, transform.position, transform.rotation) as GameObject;
            newUiSginUp.transform.SetParent(GameObject.FindGameObjectWithTag("UILogin").transform, false);
        }

        public void Login()
        {
            Close();
            GameObject newUiLogin = Instantiate(signUp, transform.position, transform.rotation) as GameObject;
            newUiLogin.transform.SetParent(GameObject.FindGameObjectWithTag("UILogin").transform, false);
        }

        public void LoginButton()
        {
            SceneManager.LoadScene(1);
        }
    }
}