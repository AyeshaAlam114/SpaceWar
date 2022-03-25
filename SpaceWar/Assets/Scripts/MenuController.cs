using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public static MenuController menu;
    public GameObject[] panels;
    public Button joinServer;
    public Button[] serverButtons;


    private void Awake()
    {
        if (MenuController.menu == null)
        {
            MenuController.menu = this;
        }
        else
        {
            if (MenuController.menu != this)
            {
                Destroy(MenuController.menu.gameObject);
                MenuController.menu = this;
            }
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public void ActivateServerButton(int button)
    {
        DeactivateServerButtons();
        serverButtons[button].gameObject.SetActive(true);
    }

    public void DeactivateServerButtons()
    {
        foreach (Button button in serverButtons)
            button.gameObject.SetActive(false);
    }
    public void OnClickCharacterPick(int whichCharacter)
    {

        //join server button enable
        joinServer.interactable = true;
        if (PlayerInfo.PI != null)
        {
            PlayerInfo.PI.mySelectedCharacter = whichCharacter;
            PlayerPrefs.SetInt("MyCharacter", whichCharacter);
        }
    }

    public void JoinServerButton()
    {
        PhotonNetwork.ConnectUsingSettings();
    }
    public void ActivatePanel(int panelIndex)
    {
        DeactivatePanels();
        panels[panelIndex].SetActive(true);
    }
    void DeactivatePanels()
    {
        foreach (GameObject panel in panels)
            panel.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        ActivatePanel(0);
        StartCoroutine(Menu());
    }

    IEnumerator Menu()
    {
        yield return new WaitForSeconds(2f);
        ActivatePanel(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
