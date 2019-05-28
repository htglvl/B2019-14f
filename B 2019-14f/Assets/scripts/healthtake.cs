using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthtake : MonoBehaviour {
	public float howmanyheal;
	public LayerMask whatishealingzone;
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		RaycastHit2D healzone = Physics2D.Raycast(transform.position, Vector2.zero, 0, whatishealingzone);
		if (healzone.collider != null)
		{
				GetComponent<healthdisplayforplayer>().notinhealingzone = false;
				
		}else
		{
				GetComponent<healthdisplayforplayer>().notinhealingzone = true;
		}
	}

}
