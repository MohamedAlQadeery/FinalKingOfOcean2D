using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingUI : MonoBehaviour
{
    private void OnEnable()
    {
        LeanTween.moveX(gameObject, 550, 1);
    }
    public void Logout()
    {
        SceneManager.LoadScene(0);
    }
    public void Close()
    {
        LeanTween.moveX(gameObject , -1100 , 1);
        SoundManager.Instance.PlaySound(SoundManager.Sound.CloseSonud);
        Destroy(gameObject,1);
    }
}
