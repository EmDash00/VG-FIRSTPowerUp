using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {

    public GameObject pauseMenu;
    public bool isPaused = false;

	// Use this for initialization
	void Start ()
    { 
        pauseMenu.SetActive(false);
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetButtonDown("Pause"))
        {
            this.isPaused = !this.isPaused;
            pauseMenu.SetActive(isPaused);
            Time.timeScale = (this.isPaused) ? 0 : 1;
        }
		
	}
}
