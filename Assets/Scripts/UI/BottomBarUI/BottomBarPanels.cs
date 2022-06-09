using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class BottomBarPanels : MonoBehaviour
{
    [SerializeField] private GameObject panelAnim;

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
        SceneManager.LoadScene(0);
    }
}
