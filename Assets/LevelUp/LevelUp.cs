using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUp : MonoBehaviour
{
    [SerializeField] GameObject lvlUi;
    private void Start()
    {
        SoundManager.Instance.PlaySound(SoundManager.Sound.LvlUpSound);
    }
    private void OnEnable()
    {
        
        LeanTween.scale(lvlUi, new Vector3(1.2f, 1.2f, 1), 1f).setLoopPingPong();
        LeanTween.scale(gameObject, new Vector3(1,1,1), 0.5f);        
    }

    public void CountinuoButton()
    {
        SoundManager.Instance.PlaySound(SoundManager.Sound.ButtonSonud);
        LeanTween.scale(gameObject, new Vector3(0, 0, 0), 1);
        Destroy(gameObject,1);
    }
}
