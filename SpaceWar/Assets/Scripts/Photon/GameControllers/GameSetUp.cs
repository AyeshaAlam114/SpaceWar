using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameSetUp : MonoBehaviour
{
    public static GameSetUp GS;
    public Text healthDisplay;
    public Transform[] spawnPoints;

    private void OnEnable()
    {
        if (GameSetUp.GS == null)
        {
            GameSetUp.GS = this;
        }
    }

    public void DisconnectPlayer()
    {
        StartCoroutine(DisconnectAndLoad());
    }
    IEnumerator DisconnectAndLoad()
    {
        //PhotonNetwork.Disconnect();
        PhotonNetwork.LeaveRoom();
       // while (PhotonNetwork.IsConnected)
       while(PhotonNetwork.InRoom)
            yield return null;
        SceneManager.LoadScene(MultiplayerSettings.multiplayerSettings.menuScene);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
