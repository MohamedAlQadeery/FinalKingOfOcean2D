using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class StartPanelUI : MonoBehaviour
{
    [SerializeField] private GameObject loginPanel;
    public void Login()
    {
        GameObject newUiLogin = Instantiate(loginPanel, transform.position, transform.rotation) as GameObject;
        newUiLogin.transform.SetParent(GameObject.FindGameObjectWithTag("UILogin").transform, false);
    }

}
