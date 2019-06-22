using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformspawner : MonoBehaviour {
	public GameObject theplatform;
	public Transform generationpoint;
	public float distancebetween;
	private float platformWidth;
	public ObjectPool theObjectpool;
	public bool objectpool;
	void Start () {
		platformWidth = theplatform.GetComponent<BoxCollider2D>().size.x;

	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.x < generationpoint.position.x)
		{
			if (objectpool == true)
			{
				
				GameObject newPlatform = theObjectpool.GetPooledObject();
				newPlatform.transform.position = transform.position;
				newPlatform.transform.rotation = transform.rotation;
				newPlatform.SetActive(true);	
			}else
			{
				transform.position = new Vector3(transform.position.x + platformWidth + distancebetween, transform.position.y, transform.position.z);
				Instantiate (theplatform, transform.position, transform.rotation);

			}
			
		}	
	}
}