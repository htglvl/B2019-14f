using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawndirt : MonoBehaviour {
	public GameObject ground;	
	private float y = 0f;
	void Start() 
	{	
         for (int x = -16; x < 40 ;x = x + 8) {
            Instantiate(ground, new Vector2(x, y), Quaternion.identity);
        }
	}
	
	// Update is called once per frame
	void Update () {
	}
	void OnBecameInvisible() {
        Destroy (gameObject);
        
        // If you would like to destroy the top most game object
		//Destroy (transfrom.root.gameObject);
    }
}
