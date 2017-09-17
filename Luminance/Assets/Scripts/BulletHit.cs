using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHit : MonoBehaviour {

    public Transform player;

    private float bulletDamage;

    public GameObject deathFlare;


    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
        Physics.IgnoreLayerCollision(9, 9);  // Pb to Pb
        Physics.IgnoreLayerCollision(9, 12); //Pb to Eb
        Physics.IgnoreLayerCollision(8, 9); // P to Pb
        Physics.IgnoreLayerCollision(11, 12); // E to Eb
        Physics.IgnoreLayerCollision(12, 12); // EB to Eb


    }

    private void Start()
    {
        bulletDamage = GetComponent<BulletStats>().damage;
    }

    private void Update()
    {
        if (transform.position.x > player.position.x + 100 || transform.position.z > player.position.z + 100 || transform.position.x < player.position.x - 100 || transform.position.z < player.position.z - 100)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision c)
    {

        if (c.collider.name == "Player")
        {
            c.collider.gameObject.GetComponent<PlayerStatus>().TakeDamage(bulletDamage);
        }
        if (c.collider.tag == "Crystal")
        {
            c.collider.gameObject.GetComponent<CrystalStats>().TakeDamage(bulletDamage);
        }
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.layer == 11)
        {
            c.gameObject.GetComponent<EnemyStats>().TakeDamage(bulletDamage);
            Destroy(gameObject);
        }

    }

    private void OnDestroy()
    {
        Instantiate(deathFlare, transform.position, transform.rotation);
    }

}
