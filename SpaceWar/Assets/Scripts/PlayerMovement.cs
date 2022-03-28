using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerMovement : MonoBehaviour//CharacterHandler
{
    //public CharacterController controller;
    //public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;


    private PhotonView PV;
    private CharacterController myCC;

    public float movementSpeed;
    public float rotationSpeed;

    public Text healthDisplay;
    CharacterHandler myChar;
    public TextMeshPro myTeamName;
    public int myTeam;

    private void Start()
    {
        PV = GetComponent<PhotonView>();
        myCC = GetComponent<CharacterController>();
        groundCheck = transform.GetChild(3).transform.GetChild(0).transform;
        //GunsFactory.GF.SetPlayer(this);
        //GunsFactory.GF.GetGun(GunType.ShotGun);
        healthDisplay = GameSetUp.GS.healthDisplay;
        myChar = GetComponent<CharacterHandler>();
       



    }

 
    public void SetTeam(int teamNum)
    {
        myTeam = teamNum;
        //if(PV.IsMine)
        GetComponent<PhotonView>().RPC("RPC_SetTeam", RpcTarget.AllBuffered, myTeam);
    }

    [PunRPC]
    void RPC_SetTeam(int teamNumber)
    {
        myTeamName.text = "Team - " + teamNumber.ToString();
    }

    void BasicMovement()
    {
        if (Input.GetKey(KeyCode.W))
        {
            myCC.Move(transform.forward * Time.deltaTime * movementSpeed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            myCC.Move(-transform.right * Time.deltaTime * movementSpeed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            myCC.Move(-transform.forward * Time.deltaTime * movementSpeed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            myCC.Move(transform.right * Time.deltaTime * movementSpeed);
        }
    }

    void BasicRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * rotationSpeed;
        transform.Rotate(new Vector3(0, mouseX, 0));
    }
    void Jump()
    {
       // Debug.Log("1");
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
           // Debug.Log("2");
            velocity.y += Mathf.Sqrt(jumpHeight * -2f * gravity);
            velocity.y += gravity * Time.deltaTime;
            myCC.Move(velocity * Time.deltaTime);
        }

        
    }
   
 
    [PunRPC]
    void RPC_Shooting()
    {
        if (myChar.GetActivatedGun() != null)
        {
            myChar.AttackByGun();
        }
        
    }

    public void HealthUpdate(int health)
    {
        healthDisplay.text = health.ToString();
    }
    [PunRPC]
    void RPC_ChangeGun()
    {
        if (Input.GetKeyDown("1"))
            this.GetComponent<GunsFactory>().GetGun(Gun.GunType.ShotGun);
        if (Input.GetKeyDown("2"))
            this.GetComponent<GunsFactory>().GetGun(Gun.GunType.SniperRifle);
        if (Input.GetKeyDown("3"))
            this.GetComponent<GunsFactory>().GetGun(Gun.GunType.MachineGun);

    }

    private void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (PV.IsMine)
        {
            
            PV.RPC("RPC_ChangeGun", RpcTarget.AllBuffered);
            BasicMovement();
            BasicRotation();
            Jump();

            if (Input.GetMouseButtonDown(0))
            {
                PV.RPC("RPC_Shooting", RpcTarget.All);
            }
        }
    }


    //// Start is called before the first frame update
    //void Start()
    //{
    //    groundCheck = transform.GetChild(1).transform.GetChild(0).transform;
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

    //    if (GetComponent<PhotonView>().IsMine)
    //    {
    //        if (isGrounded && velocity.y < 0)
    //        {
    //            velocity.y = -2f;
    //        }

    //        Move();
    //        Jump();

    //    }


    //}

    //void Move()
    //{
    //    float xPosition = Input.GetAxis("Horizontal");
    //    float zPosition = Input.GetAxis("Vertical");

    //    Vector3 move = transform.right * xPosition + transform.forward * zPosition;
    //    controller.Move(move * speed * Time.deltaTime);
    //}
   
}
