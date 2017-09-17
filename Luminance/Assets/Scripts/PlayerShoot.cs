using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour {

    public GameObject bullet;
    public Transform pointToShoot;
    public GameObject flare;

    public GameObject parentPlayer;

    public float range = 100f;
    public float speed = 10f;

    public LayerMask player;
    public LayerMask bullets;


    public float fireDelta = 0.2F; //fire rate

    private float nextFire = 0.2F; //after first shot
    private float myTime = 0.0F;

    private void Start()
    {
        
    }

    private void Update()
    {
        myTime = myTime + Time.deltaTime;

        if (Input.GetButton("Fire1"))
        {
            if (myTime > nextFire)
            {
                nextFire = myTime + fireDelta;
                Shoot();

                // create code here that animates the newProjectile

                nextFire = nextFire - myTime;
                myTime = 0.0F;
            }
        }
    }

    void Shoot()
    {
        Instantiate(flare, pointToShoot.position, parentPlayer.transform.rotation);
        GameObject clone = Instantiate(bullet, pointToShoot.position, parentPlayer.transform.rotation);
        

        clone.GetComponent<Rigidbody>().velocity = transform.forward * speed;
        

    }
}
