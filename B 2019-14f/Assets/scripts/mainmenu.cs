using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainmenu : MonoBehaviour {

public Animator animator;
	public void playgame()
	{
		SceneManager.LoadScene("Main");
	} 
	public void quitgame()
	{
		Application.Quit();
		Debug.Log("quiting");
	}
	public void Setting()
	{
		animator.SetBool("goToSetting", true);
	}
	public void Back()
	{
		animator.SetBool("goToSetting", false);
	}
	
}
