using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomBarUI : MonoBehaviour
{

    [SerializeField] private GameObject settingPanel;

    public void Setting()
    {
        GameObject newUiLogin = Instantiate(settingPanel, transform.position, transform.rotation) as GameObject;
        newUiLogin.transform.SetParent(GameObject.FindGameObjectWithTag("CanvasUI").transform, false);
    }

}
