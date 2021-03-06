using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomBarUI : MonoBehaviour
{

    [SerializeField] private GameObject settingPanel;
    [SerializeField] GameObject friendsPanel;
    public void Setting()
    {
        SoundManager.Instance.PlaySound(SoundManager.Sound.ButtonSonud);
        GameObject newUiLogin = Instantiate(settingPanel, settingPanel.transform.position, settingPanel.transform.rotation) as GameObject;
        newUiLogin.transform.SetParent(GameObject.FindGameObjectWithTag("CanvasUI").transform, false);       
    }


    public void OnClickFriends()
    {
        SoundManager.Instance.PlaySound(SoundManager.Sound.ButtonSonud);
        GameObject friendPanelUI = Instantiate(friendsPanel, friendsPanel.transform.position, friendsPanel.transform.rotation) ;
        friendPanelUI.transform.SetParent(GameObject.FindGameObjectWithTag("CanvasUI").transform, false);
    }
}
