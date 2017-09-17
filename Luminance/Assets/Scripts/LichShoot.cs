using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LichShoot : MonoBehaviour
{

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

    private float myTime = 0.0F;

    public Animator anim;
    public bool animationPlaying;

    private void Start()
    {
        target = GameObject.Find("Player");
        bullet = bulletPrefab;

        animationPlaying = false;

        InvokeRepeating("CheckToShoot", 0, .2f);

        Random.InitState((int)Time.time);

    }

    // Update is called once per frame
    void Update()
    {

        distance = Vector3.Distance(target.transform.position, transform.position);

        partToRotate.transform.LookAt(target.transform);

        myTime = myTime + Time.deltaTime;


    }

    void CheckToShoot()
    {
        if (distance > 25)
        {
            StopAllCoroutines();
            animationPlaying = false;

            return;
        }


        if (!animationPlaying)
        {
            myTime = 0.0F;
            StartCoroutine(Shoot());
        }
    }

    public virtual IEnumerator Shoot()
    {
        int attack = Random.Range(1, 3);

        animationPlaying = true;

        if (attack == 1)
        {

            anim.SetTrigger("attack01");

            while(myTime < .8f)
            {
                Instantiate(flare, pointToShoot.position, transform.rotation);
                GameObject clone = Instantiate(bullet, pointToShoot.position, partToRotate.transform.rotation);

                clone.GetComponent<Rigidbody>().velocity = pointToShoot.forward * speed;

                yield return new WaitForSeconds(fireDelta);
            }

            yield return null;

        }
        else if (attack == 2)
        {
            anim.SetTrigger("attack02");

            while (myTime < 1.8f)
            {

                Instantiate(flare, pointToShoot.position, transform.rotation);
                GameObject clone = Instantiate(bullet, pointToShoot.position, partToRotate.transform.rotation);

                clone.GetComponent<Rigidbody>().velocity = pointToShoot.forward * speed;

                yield return new WaitForSeconds(fireDelta);

            }

            yield return null;
        }

        animationPlaying = false;
        yield return 0;
    }
}
