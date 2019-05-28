using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pauseMenu : MonoBehaviour {

	// Use this for initialization
	public static bool gameispause = false; 
	public GameObject PauseMenuUI;
	public Animator anim;

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if ( gameispause )
			{
				
				Resume();
			}else
			{
				
				Pause();
			}
		}
	}

	public void Resume () 
	{ 
		anim.SetBool("ispause", false);
		Time.timeScale = 1f;
		gameispause = false;
	}
	public void pausemenu ()
	{
		PauseMenuUI.SetActive(false);
	}

	public void Pause ()
	{
		anim.SetBool("ispause", true);
		PauseMenuUI.SetActive(true);
		Time.timeScale = 0f;
		gameispause = true;
	}

	public void LoadMenu()
	{
		SceneManager.LoadScene("Menu");
		Time.timeScale = 1f;
	}

	public void Setting()
	{
		SceneManager.LoadScene("Setting");
		Time.timeScale = 1f;
	}

	public void QuitGame()
	{
		Debug.Log("Quiting ...");
		Application.Quit();
		Time.timeScale = 1f;
	}
}







