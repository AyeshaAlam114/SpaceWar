using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AvatarCombac : MonoBehaviour
{
    private PhotonView PV;
    private AvatarSetUp avatarSetup;
    public Transform rayOrigin;

    public Text healthDisplay;


    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
        avatarSetup = GetComponent<AvatarSetUp>();
        healthDisplay = GameSetUp.GS.healthDisplay;
    }

    // Update is called once per frame
    void Update()
    {
        if (!PV.IsMine)
        {
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
            PV.RPC("RPC_Shooting", RpcTarget.All);
        }
        healthDisplay.text = avatarSetup.playerHealth.ToString();
    }

    [PunRPC]
    void RPC_Shooting()
    {
        RaycastHit hit;
        if (Physics.Raycast(rayOrigin.position, rayOrigin.TransformDirection(Vector3.forward), out hit, 1000))
        {
            Debug.DrawRay(rayOrigin.position, rayOrigin.TransformDirection(Vector3.forward) * hit.distance, Color.green);
            Debug.Log("Did Hit");
            if (hit.transform.CompareTag("Avatar"))
            {
                hit.transform.gameObject.GetComponent<AvatarSetUp>().playerHealth -= avatarSetup.playerDamage;
            }
        }
        else
        {
            Debug.DrawRay(rayOrigin.position, rayOrigin.TransformDirection(Vector3.forward) * 1000, Color.yellow);
            Debug.Log("Did not Hit");
        }
    }
}
