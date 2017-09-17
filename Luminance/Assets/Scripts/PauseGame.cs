using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour {

    public GameObject pauseMenu;
    public GameObject player;
    public bool isPaused;

    private void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            isPaused = !isPaused;
            Time.timeScale = ((isPaused) ? 0 : 1);
            pauseMenu.SetActive(isPaused);
            player.GetComponent<PlayerShoot>().enabled = !isPaused;
            
        }
    }

    public void ResumeGame()
    {
        isPaused = !isPaused;
        Time.timeScale = ((isPaused) ? 0 : 1);
        pauseMenu.SetActive(isPaused);
        player.GetComponent<PlayerShoot>().enabled = !isPaused;
    }
}
