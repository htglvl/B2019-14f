using UnityEngine;
using UnityEngine.SceneManagement;

public class gamemanager : MonoBehaviour {
	private bool gamehasended = false;
	public GameObject DeadMenuUI, controllerUI;
	public void endgame()
	{if (gamehasended == false)
		{
			gamehasended = true;
			Debug.Log("game over");
			DeadMenuUI.SetActive(true);	
			controllerUI.SetActive(false);
			Time.timeScale = 0f;
			if (Input.GetKeyDown(KeyCode.R))
			{
					restart();
					Time.timeScale = 1f;
			}
		}
	}
	public void restart()
	{
		DeadMenuUI.SetActive(false);
		controllerUI.SetActive(true);
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		Time.timeScale = 1f;
	}
}
