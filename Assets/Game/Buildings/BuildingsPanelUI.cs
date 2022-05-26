using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BuildingsPanelUI : MonoBehaviour
{
    [SerializeField] private GameObject storgePanel;
    [SerializeField] private Button storgeButton;

    [SerializeField] private GameObject shipsPanel;
    [SerializeField] private Button shipsButton;

    [SerializeField] private GameObject guildPanel;
    [SerializeField] private Button guildButton;

    [SerializeField] private GameObject rocketPanel;
    [SerializeField] private Button roketButton;

    [SerializeField] private GameObject shopPanel;
    [SerializeField] private Button shopButton;


    void Start()
    {
        storgeButton.onClick.AddListener(Storge);
        shipsButton.onClick.AddListener(Ships);
        guildButton.onClick.AddListener(Guild);
        roketButton.onClick.AddListener(Rocket);
        shopButton.onClick.AddListener(Shop);
    }

    private void Storge()
    {
        SoundManager.Instance.PlaySound(SoundManager.Sound.ButtonSonud);
        GameObject newUiLogin = Instantiate(storgePanel, transform.position, transform.rotation) as GameObject;
        newUiLogin.transform.SetParent(GameObject.FindGameObjectWithTag("GameUI").transform, false);
    }

    private void Ships()
    {
        SoundManager.Instance.PlaySound(SoundManager.Sound.ButtonSonud);
        GameObject newUiLogin = Instantiate(shipsPanel, transform.position, transform.rotation) as GameObject;
        newUiLogin.transform.SetParent(GameObject.FindGameObjectWithTag("GameUI").transform, false);
    }

    private void Guild()
    {
        SoundManager.Instance.PlaySound(SoundManager.Sound.ButtonSonud);
        GameObject newUiLogin = Instantiate(guildPanel, transform.position, transform.rotation) as GameObject;
        newUiLogin.transform.SetParent(GameObject.FindGameObjectWithTag("GameUI").transform, false);
    }

    private void Rocket()
    {
        SoundManager.Instance.PlaySound(SoundManager.Sound.ButtonSonud);
        GameObject newUiLogin = Instantiate(rocketPanel, transform.position, transform.rotation) as GameObject;
        newUiLogin.transform.SetParent(GameObject.FindGameObjectWithTag("GameUI").transform, false);
    }

    private void Shop()
    {
        SoundManager.Instance.PlaySound(SoundManager.Sound.ButtonSonud);
        GameObject newUiLogin = Instantiate(shopPanel, transform.position, transform.rotation) as GameObject;
        newUiLogin.transform.SetParent(GameObject.FindGameObjectWithTag("GameUI").transform, false);
    }

}
