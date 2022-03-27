using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bullet : MonoBehaviour
{
    public float maxBoundary;
    Gun firedFromGun;
    int ownTeam;
    private AvatarSetUp avatarSetup;
   


    public void SetFiredFromGun(Gun gun)
    {
        firedFromGun = gun;
        ownTeam = firedFromGun.transform.root.gameObject.GetComponent<CharacterHandler>().myTeam;
        avatarSetup = firedFromGun.transform.root.gameObject.GetComponent<AvatarSetUp>();
    }
    // Update is called once per frame
    void Update()
    {
        //if (transform.position.y > maxBoundary)                     //check if bullet reaches the upper boundary 
        //    Destroy(gameObject);                                    //destroy bullet when it cross boundary
    }
    private void OnTriggerEnter(Collider other)
    {
       
        if (ownTeam != other.gameObject.GetComponent<CharacterHandler>().myTeam && other.gameObject.CompareTag("Avatar"))           //check if the hitted object is oop team or not
        {
            Debug.Log("hit");
            //other.gameObject.GetComponent<AvatarSetUp>().playerHealth -= avatarSetup.playerDamage;
            //healthDisplay.text = avatarSetup.playerHealth.ToString();
            int damagePower = firedFromGun.GetGunPower();
            other.GetComponent<CharacterHandler>().GetDamage(damagePower);      //if it is from opp team then decrease other's health
            //other.gameObject.GetComponent<CharacterHandler>().health = other.GetComponent<CharacterHandler>().health;
            //other.gameObject.GetComponent<PlayerMovement>().HealthUpdate();
            Destroy(gameObject);                                                //after decreasing obstacle's health destroy bullet
        }

    }
    //private void OnCollisionEnter(Collision collision)
    //{
    //    Destroy(this.gameObject);
    //}

}
