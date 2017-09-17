using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLevel : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Time.timeScale = 0;	
	}

    public void Continue()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}
