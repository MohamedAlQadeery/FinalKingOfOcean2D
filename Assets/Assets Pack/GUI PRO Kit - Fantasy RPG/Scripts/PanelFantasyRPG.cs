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
        [SerializeField] private GameObject panelAnim;
        

        public void OnEnable()
        {
            LeanTween.moveY(panelAnim, 1200, 0.5f);
        }

        public void Close()
        {
            LeanTween.moveY(panelAnim, -965, 0.5f);
            SoundManager.Instance.PlaySound(SoundManager.Sound.CloseSonud);
            Destroy(gameObject,0.5f);
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