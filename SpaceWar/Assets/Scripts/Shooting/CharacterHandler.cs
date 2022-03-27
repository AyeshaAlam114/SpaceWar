using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHandler : Gun
{
    bool once=true;
   // public GameObject bombPrefab;
    public int health;
   // SpawnManager spawnManager;
    public GameObject activatedGun=null;
    public GameObject gunPosition;
    public int myTeam;


    // Start is called before the first frame update
    void Start()
    {
         once = true;
        SetCharacter(this);
        SetHealth(200);
       

    }

    // Update is called once per frame
    void Update()
    {
        Die();
        
    }


    public virtual void AttackByGun()
    {
        GetBomb();
    }
    public void GetBomb()
    {
        
        GetActivatedGun().GetComponent<Gun>().Fire();
    }

    //public void SetSpawnManager(SpawnManager spawnManagerref)
    //{
    //    spawnManager = spawnManagerref;
    //}

    //public void FireBomb()
    //{
    //    this.GetComponent<CharacterHandler>().ThrowBomb();
    //}
    public void SetHealth(int Health)
    {
        health=Health;
        GetComponent<PlayerMovement>().HealthUpdate(health);
    }
    public int GetHealth()
    {
        return health;
    }

    public void GetDamage(int damageByPower)
    {
        this.health -= damageByPower;
        GetComponent<PlayerMovement>().HealthUpdate(health);
        if (health < 0)
            DieByHit();
    }

    void DieByHit()
    {
        //HideMe();
        Invoke(nameof(DestroyMe), 2f);
        //if (this.CompareTag("Enemy"))
        //    StartCoroutine(EnemySpawn());

    }

    //void HideMe()
    //{
    //    this.GetComponent<MeshRenderer>().enabled = false;
    //    this.GetComponent<BoxCollider>().enabled = false;

    //}
    //IEnumerator EnemySpawn()
    //{
    //    yield return new WaitForSeconds(3);
    //    if (once)
    //    {
    //        //EnemySpawning();
            
    //        once = false;
    //    }
    //}

    //void EnemySpawning()
    //{
    //    spawnManager.SpawnEnemy(System.Array.IndexOf(spawnManager.spawnPositions, this.gameObject.transform.parent.transform));
    //    Invoke(nameof(DestroyMe), 2f);
    //}
    //public virtual void ThrowBomb()
    //{


    //}

   

    public void SetActivatedGun(GameObject activeGun)
    {
        Debug.Log("gun is setted");
        Debug.Log(activatedGun);
        activatedGun = activeGun;
        Debug.Log(activatedGun.name);
       // Debug.Log("gun is setted 22");
    }
    public GameObject GetActivatedGun()
    {
        return activatedGun;
    }

  

    void Die()
    {
        if (once)
        {
            if (transform.position.y < -5)
                DestroyMe();
        }
    }

    void DestroyMe()
    {
        //int i = camera.target.IndexOf(this.gameObject.transform);
        //camera.target.RemoveAt(i);

        Destroy(gameObject);
    }

}
