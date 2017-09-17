using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CW_Movement : MonoBehaviour {

    public float speed;
    public GameObject target;


    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("PlayerTarget");

    }

    // Update is called once per frame
    void Update () {

        if (Vector3.Distance(target.transform.position, transform.position) > 25)
            return;

        float step = speed * Time.deltaTime;

        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);
	}
}
