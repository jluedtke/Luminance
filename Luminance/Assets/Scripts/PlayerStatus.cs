using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour {

    public float maxHp = 100;
    public float currentHp;
    public float totalLumens = 0;

    public GameObject lumenCounter;
    private Text lumenText;
    public Light playerLight;


    private bool playerHasDied;
    public GameObject gameOverMenu;

    private Animator anim;

    private PlayerDeath PD;

	// Use this for initialization
	void Start () {
        if (lumenCounter == null)
            lumenCounter = GameObject.Find("Canvas/LumenCounter");

        lumenText = lumenCounter.GetComponent<Text>();
        currentHp = maxHp;
        totalLumens = 0;

        playerHasDied = false;

        playerLight = GetComponentInChildren<Light>();

        anim = GetComponent<Animator>();

        PD = GetComponent<PlayerDeath>();

    }

    // Update is called once per frame
    void Update () {

        playerLight.transform.position = new Vector3(playerLight.transform.position.x, playerLight.transform.position.y - (Time.deltaTime / 10), playerLight.transform.position.z);
        playerLight.intensity -= Time.deltaTime * .04f;



    }

    private void FixedUpdate()
    {
        if (playerLight.transform.position.y < 4 || playerLight.intensity < 1.5)
        {
            anim.SetBool("isAt25", true);
            PD.PlayerAt25();
        }
        else
        {
            anim.SetBool("isAt25", false);
            PD.PlayerAbove25();

        }

        if (playerLight.intensity < 0 || playerLight.intensity == 0 && !playerHasDied)
        {
            PlayerDied();

        }
    }

    public void TakeDamage(float damage)
    {
        float yPos = playerLight.transform.position.y - (damage/10);

        playerLight.transform.position = new Vector3(playerLight.transform.position.x, yPos, playerLight.transform.position.z);
        playerLight.intensity -= damage * .04f;
    }

    public void AddLumen()
    {
        anim.SetTrigger("isHealing");
        //2 for 2%
        float yPos = playerLight.transform.position.y + .4f;
        float yPosClamped = Mathf.Clamp(yPos, 0f, 9);

        playerLight.transform.position = new Vector3(playerLight.transform.position.x, yPosClamped, playerLight.transform.position.z);
        playerLight.intensity += 4 * .04f;

        if (playerLight.intensity > 4)
            playerLight.intensity = 4;

        totalLumens++;
        lumenText.text = totalLumens.ToString();

    }

    void PlayerDied()
    {
        playerHasDied = true;

        PD.PlayerHasDied();

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (enemies.Length > 0)
        {
            for (int i = 0; i < enemies.Length; i++)
            {
                if (enemies[i].name.Contains("Lich"))
                {
                    StopAllCoroutines();
                    enemies[i].GetComponent<LichShoot>().animationPlaying = true;
                    enemies[i].GetComponent<LichShoot>().enabled = false;
                }
                else
                {
                    enemies[i].GetComponent<CW_Movement>().enabled = false;
                    enemies[i].GetComponent<EnemyShoot>().enabled = false;

                }
            }

        }

        gameOverMenu.SetActive(true);
        GetComponentInChildren<PlayerShoot>().enabled = false;
        GetComponent<PlayerMovement>().enabled = false;
        GetComponent<Rigidbody>().freezeRotation = true;
        GetComponent<Rigidbody>().velocity = Vector3.zero;



    }
}
//Jared sucks potatos