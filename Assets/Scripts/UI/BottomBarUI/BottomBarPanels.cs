using FishGame.Core;
using PlayFab;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class BottomBarPanels : MonoBehaviour
{
    [SerializeField] private GameObject panelAnim;
    [SerializeField] GameObject confirmPanel;
    public void OnEnable()
    {
        LeanTween.moveX(panelAnim, 540, 0.5f);
    }
    public void Close()
    {
        LeanTween.moveX(panelAnim, -600, 0.5f);
        SoundManager.Instance.PlaySound(SoundManager.Sound.CloseSonud);
        Destroy(gameObject, 0.5f);
    }

    public void SignOut()
    {
        SoundManager.Instance.PlaySound(SoundManager.Sound.ButtonSonud);
        confirmPanel.SetActive(true);
    }

    public void SignOutConfirm()
    {
        SoundManager.Instance.PlaySound(SoundManager.Sound.CloseSonud);
        SceneManager.LoadScene(0);
    }

    public void CancelSignOut()
    {
        SoundManager.Instance.PlaySound(SoundManager.Sound.CloseSonud);
        confirmPanel.SetActive(false);
    }
}
