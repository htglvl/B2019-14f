using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class scoresystem : MonoBehaviour {

	public TextMeshProUGUI score, hiscore;
	public int normalscore;
	public int highscore;
	public Transform player;
	// Update is called once per frame

	void Start()
	{
		normalscore = 0;
		highscore = PlayerPrefs.GetInt("BestScore",0);
	}
		void Update () {
		if (player != null)
		{
			normalscore = Mathf.RoundToInt(player.position.x / 10);
			score.text = normalscore.ToString();
			hiscore.text = "High Score: " +  highscore.ToString();
			if (normalscore > highscore)
			{
				highscore = normalscore;
				PlayerPrefs.SetInt("BestScore", highscore);
			}
		}
	}
}
