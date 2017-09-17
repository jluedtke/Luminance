using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevel : MonoBehaviour {

    public GameObject endPanel;

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            endPanel.SetActive(true);
            other.gameObject.GetComponent<PlayerMovement>().enabled = false;
            other.GetComponentInChildren<PlayerShoot>().enabled = false;
            Time.timeScale = 0;
        }
    }
}
