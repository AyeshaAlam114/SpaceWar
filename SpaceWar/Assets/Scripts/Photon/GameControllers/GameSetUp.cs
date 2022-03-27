using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameSetUp : MonoBehaviour
{
    public static GameSetUp GS;
    public Text healthDisplay; /////////////////////sir se puchna h

    public int nextPlayersTeam;

    public Transform[] spawnPointsTeamOne;
    public Transform[] spawnPointsTeamTwo;

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

    public void UpdateTeam()
    {
        if (nextPlayersTeam == 1)
        {
            nextPlayersTeam = 2;
        }
        else
        {
            nextPlayersTeam = 1;
        }
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
