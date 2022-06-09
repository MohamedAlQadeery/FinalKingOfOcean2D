using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
namespace LayerLab.FantasyRPG
{
    public class PanelFantasyRPG : MonoBehaviour
    {
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
    }
}