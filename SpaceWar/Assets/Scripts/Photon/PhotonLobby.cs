using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonLobby : MonoBehaviourPunCallbacks
{
    public static PhotonLobby lobby;

    //public GameObject battleButton;
    //public GameObject cancelButton;

    private void Awake()
    {
        lobby = this;       //create a singleton 
    }

    // Start is called before the first frame update
    void Start()
    {
       /* PhotonNetwork.ConnectUsingSettings(); */      //connects to MAster client.
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Player has connected with photon master server.");
        PhotonNetwork.AutomaticallySyncScene = true;
        MenuController.menu.ActivateServerButton(1);
    }
    public void OnBattleButtonClicked()
    {
        Debug.Log("Battle button was clicked");
        MenuController.menu.ActivateServerButton(2);
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Tried to join a random room but failed. There must be no open games available");
        CreateRoom();
    }
    void CreateRoom()
    {
        Debug.Log("Trying to create a new room");

        int randomRoomName = Random.Range(0, 10000);
        RoomOptions roomOps = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = 10 };
        PhotonNetwork.CreateRoom("Room" + randomRoomName, roomOps);

    }

  
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Tried to create room but failed, there must already be a room with the same name");
        CreateRoom();
    }
    public void OnCancelButtonClicked()
    {
        Debug.Log("Cancel button was clicked");
        MenuController.menu.ActivateServerButton(1);
        PhotonNetwork.LeaveRoom();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
