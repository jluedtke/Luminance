using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lumen_Movement : MonoBehaviour {

    public float speed;
    public GameObject target;


    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("PlayerTarget");

    }

    // Update is called once per frame
    void Update()
    {

        if (Vector3.Distance(target.transform.position, transform.position) > 7)
            return;

        float step = speed * Time.deltaTime;

        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);
    }

    private void OnTriggerEnter(Collider c)
    {
        if (c.name == "Player")
        {
            target.GetComponentInParent<PlayerStatus>().AddLumen();
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        //Play audio
    }
}
