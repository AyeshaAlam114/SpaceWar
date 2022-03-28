using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


public class PhotonPlayer : MonoBehaviour
{
    public PhotonView PV;
    public GameObject myAvatar;
    public int myTeam;

    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
        if (PV.IsMine)
        {
            PV.RPC("RPC_GetTeam", RpcTarget.MasterClient);
        }

    }
    [PunRPC]
    void RPC_GetTeam()
    {
        myTeam = GameSetUp.GS.nextPlayersTeam;
        GameSetUp.GS.UpdateTeam();
        PV.RPC("RPC_SentTeam", RpcTarget.OthersBuffered, myTeam);
    }

    [PunRPC]
    void RPC_SentTeam(int whichTeam)
    {
        myTeam = whichTeam;
    }

    [PunRPC]
    void RPC_SentTeamToAvatar(int whichTeam)
    {
        if(myAvatar!=null)
        myAvatar.GetComponent<PlayerMovement>().SetTeam(whichTeam);
       // myTeam = whichTeam;
    }

    // Update is called once per frame
    void Update()
    {
        if (myAvatar == null && myTeam!=0)
        {
            if (myTeam == 1)
            {
                int spawnPicker = Random.Range(0, GameSetUp.GS.spawnPointsTeamOne.Length);
                if (PV.IsMine)
                {
                    myAvatar = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerAvatar"),
                         GameSetUp.GS.spawnPointsTeamOne[spawnPicker].position,
                         GameSetUp.GS.spawnPointsTeamOne[spawnPicker].rotation, 0);
                  
                   // myAvatar.transform.GetChild(1).GetComponent<TextMeshPro>().text = "Team 1";
                    //myAvatar.GetComponent<PlayerMovement>().SetTeam(myTeam);
                    PV.RPC("RPC_SentTeamToAvatar", RpcTarget.AllBuffered, myTeam);
                }
            }
            else
            {
                int spawnPicker = Random.Range(0, GameSetUp.GS.spawnPointsTeamTwo.Length);
                if (PV.IsMine)
                {
                    myAvatar = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerAvatar"),
                         GameSetUp.GS.spawnPointsTeamTwo[spawnPicker].position,
                         GameSetUp.GS.spawnPointsTeamTwo[spawnPicker].rotation, 0);
                    PV.RPC("RPC_SentTeamToAvatar", RpcTarget.AllBuffered, myTeam);
                    // myAvatar.transform.GetChild(1).GetComponent<TextMeshPro>().text = "Team 2";
                    // myAvatar.GetComponent<PlayerMovement>().SetTeam(myTeam);
                }

            }
        }

    }
}
