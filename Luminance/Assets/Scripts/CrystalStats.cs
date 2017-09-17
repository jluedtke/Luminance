using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalStats : MonoBehaviour {

    public float maxHp = 30;
    public float currentHp;
    public float dmgRes = 0;

    public GameObject deathAnim;
    public GameObject deathAnim2;

    public GameObject enemyPrefab;
    // Use this for initialization
    void Start()
    {
        currentHp = maxHp;
    }


    public void TakeDamage(float damageTaken)
    {
        float damageToTake = (damageTaken - dmgRes);
        if (damageToTake < 1)
            damageToTake = 1;

        currentHp -= damageToTake;

        if (currentHp < 1)
        {
            //Play animation then
            Instantiate(deathAnim, transform.position, transform.rotation);
            Instantiate(deathAnim2, transform.position + new Vector3(.5f, 0, 0), transform.rotation);

            int chanceToSpawn = Random.Range(1, 11);

            if (chanceToSpawn < 2)
            {
                Instantiate(enemyPrefab, new Vector3(transform.position.x, .8f, transform.position.z), Quaternion.identity);
            }
            else
            {
                GetComponent<CreateLumens>().ObjectDied(1, 3);

            }


            Destroy(gameObject);
        }
    }
}
