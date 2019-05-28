using UnityEngine;
using UnityEngine.SceneManagement;


public class playerstat : MonoBehaviour {
	public int curhealth;
	public int maxhealth = 100;
	// Use this for initialization
	void Start () {
		curhealth = maxhealth;
	}
	void die () 
	{
	SceneManager.LoadScene(1);
	}
}
