using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStats : MonoBehaviour {

    public float maxHp = 100;
    public float currentHp;
    public float dmgRes = 5;

    public GameObject deathAnim;

    public Animator anim;

    public int minLumen;
    public int maxLumen;

    private bool alreadyDead;

	// Use this for initialization
	void Start () {
        currentHp = maxHp;

        if (GetComponentInChildren<Animator>())
            anim = GetComponentInChildren<Animator>();

        alreadyDead = false;

	}
	

    public void TakeDamage(float damageTaken)
    {
        float damageToTake = (damageTaken - dmgRes);
        if (damageToTake < 1)
            damageToTake = 1;

        currentHp -= damageToTake;

        if (currentHp < 1)
        {
            if (gameObject.name.Contains("Lich") == false)
            {
                Instantiate(deathAnim, transform.position, transform.rotation);
                GetComponent<CreateLumens>().ObjectDied(minLumen, maxLumen);
                Destroy(gameObject);

            }
            else
            {

                if (!alreadyDead)
                {
                    StopAllCoroutines();
                    GetComponent<LichShoot>().animationPlaying = true;
                    GetComponent<LichShoot>().enabled = false;
                    alreadyDead = true;

                    anim.SetBool("isDead", true);
                    GetComponent<CreateLumens>().ObjectDied(minLumen, maxLumen);
                    Destroy(gameObject, 1.5f);

                }
            }
        }
    }

    private void OnDestroy()
    {
        if (gameObject.name.Contains("Lich"))
        {
            return;

        }
        Instantiate(deathAnim, transform.position, transform.rotation);         
    }
}
