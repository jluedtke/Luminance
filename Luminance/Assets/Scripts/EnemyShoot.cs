using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour {

    public GameObject target;
    public GameObject bulletPrefab;
    private GameObject bullet;


    public GameObject partToRotate;
    private float distance;

    public Transform pointToShoot;
    public GameObject flare;

    public float range = 100f;
    public float speed = 15f;


    public float fireDelta = 0.5F; //fire rate

    private float nextFire = 0.5F;
    private float myTime = 0.0F;


    private void Start()
    {
        target = GameObject.Find("Player");
        bullet = bulletPrefab;
    }

    // Update is called once per frame
    void Update () {

        distance = Vector3.Distance(target.transform.position, transform.position);

        if (distance >25)
            return;


        partToRotate.transform.LookAt(target.transform);

        myTime = myTime + Time.deltaTime;

        if (myTime > nextFire)
        {
            nextFire = myTime + fireDelta;
            Shoot();

            // create code here that animates the newProjectile

            nextFire = nextFire - myTime;
            myTime = 0.0F;
        }

    }

    private void Shoot()
    {
        Instantiate(flare, pointToShoot.position, transform.rotation);
        GameObject clone = Instantiate(bullet, pointToShoot.position, partToRotate.transform.rotation);

        clone.GetComponent<Rigidbody>().velocity = pointToShoot.forward * speed;

    }
}
