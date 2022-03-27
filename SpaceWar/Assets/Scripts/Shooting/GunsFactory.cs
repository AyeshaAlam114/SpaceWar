using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GunsFactory : MonoBehaviour
{
    //public static GunsFactory GF;

    private PhotonView PV;
    public List<GameObject> gunsList;
    public int mySelectedGun;
    CharacterHandler player;
    public GameObject spawnedGun;


    private void Start()
    {
        PV = GetComponent<PhotonView>();
         //if (PlayerPrefs.HasKey("MyGun"))
         //{
         //    mySelectedGun = PlayerPrefs.GetInt("MyGun");
         //}
         //else
         //{
         mySelectedGun = 0;
            PlayerPrefs.SetInt("MyGun", mySelectedGun);
        //}
    }
    public void GetGun(Gun.GunType type)
    {
        PlayerPrefs.SetInt("MyGun", (int)type);
        switch (type)
        {
            case Gun.GunType.ShotGun:
                PV.RPC("RPC_InstantiateGun", RpcTarget.All,0);
                break;          
            case Gun.GunType.SniperRifle:
                PV.RPC("RPC_InstantiateGun", RpcTarget.All, 1);
                break;          
            case Gun.GunType.MachineGun:
                PV.RPC("RPC_InstantiateGun", RpcTarget.All, 2);
                break;
            default:
                PV.RPC("RPC_InstantiateGun", RpcTarget.All, 0);
                break;

        }
    }

    public void SetPlayer(CharacterHandler Player)
    {
       // Debug.Log("set player - " + player);
        player = Player;
       // Debug.Log("set player 2 - " + player.name);
    }
    //public void SetPlayer(TeamTwoPlayer Player)
    //{
    //    player = Player;
    //}

    void DestroyPreviousGun()
    {
        Destroy(spawnedGun);
    }

    [PunRPC]
    public void RPC_InstantiateGun(int index)
    {
        if (spawnedGun != null)
            {
                Vector3 gunPosition = spawnedGun.transform.position;
                Quaternion gunRotation = spawnedGun.transform.rotation;
                Transform gunParent = spawnedGun.transform.parent;
                DestroyPreviousGun();
                spawnedGun = Instantiate(gunsList[index], gunPosition, gunRotation, gunParent);
                spawnedGun.transform.parent.gameObject.GetComponent<GunAvatarSetUp>().myGun = spawnedGun;
                SetGunToPlayer(spawnedGun);
            }
        
           
       // DestroyPreviousGun();
       //// spawnedGun=PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PhotonNetworkGun"), SetGunPosition(), player.transform.rotation);
       // //spawnedGun.transform.parent = player.transform;
       // spawnedGun = Instantiate(gunsList[index], SetGunPosition(), player.transform.rotation, player.transform);
       // SetGunToPlayer(spawnedGun);
    }

    public void SetGunToPlayer(GameObject gun)
    {
        Debug.Log("Gun Factory - "+gun.name);
        Debug.Log(player);
        player.SetActivatedGun(gun);
    }

    //Vector3 SetGunPosition()
    //{
    //    return player.gunPosition.position;
    //}
}
