using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class platformdestroy : MonoBehaviour {
	public GameObject pointToDestroy;
	public bool objectpol;
	// Use this for initialization
	void Start () {
		pointToDestroy = GameObject.Find("dirt destroy point");
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.x < pointToDestroy.transform.position.x)
		{
			if (objectpol == true)
			{
				gameObject.SetActive(false);	
			}else
			{
				Destroy(gameObject);
			}
			
			
		}
	}
}
