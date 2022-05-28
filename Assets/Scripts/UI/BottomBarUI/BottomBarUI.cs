using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomBarUI : MonoBehaviour
{

    [SerializeField] private GameObject settingPanel;    
    public void Setting()
    {
        SoundManager.Instance.PlaySound(SoundManager.Sound.ButtonSonud);
        GameObject newUiLogin = Instantiate(settingPanel, settingPanel.transform.position, settingPanel.transform.rotation) as GameObject;
        newUiLogin.transform.SetParent(GameObject.FindGameObjectWithTag("CanvasUI").transform, false);       
    }

}
