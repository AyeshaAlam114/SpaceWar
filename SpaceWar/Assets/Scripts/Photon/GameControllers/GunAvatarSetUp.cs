using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunAvatarSetUp : MonoBehaviour
{
    public PhotonView PV;

    public int gunValue;
    public GameObject myGun;

    //// Start is called before the first frame update
    //void Start()
    //{
        
    //    if (PV.IsMine)
    //    {
    //        Debug.Log("llllllllllllllllllllllllll");
    //       // PV.RPC("RPC_AddGun", RpcTarget.AllBuffered, GunsFactory.GF.mySelectedGun);
    //        PV.RPC("RPC_AddGun", RpcTarget.All, 0);
    //    }
       
    //}

    //[PunRPC]
    public void AddGun(int whichGun)
    {
        gunValue = whichGun;
        GunsFactory gunFactory = transform.root.GetComponent<GunsFactory>();
        myGun = Instantiate(gunFactory.gunsList[gunValue], transform.position, transform.rotation, transform);
        //GunsFactory.GF.InstantiateGun(gunValue);
        gunFactory.spawnedGun= myGun;
        Debug.Log("Add Gun - " + myGun.name);
        gunFactory.SetGunToPlayer(myGun);
       // myGun = Instantiate(GunsFactory.GF.[whichGun], transform.position, transform.rotation, transform);

    }
}
