using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class legCollide : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	void OnCollisionEnter2D(Collision2D col)
    {
        GetComponent<movement>().animator.SetBool("isjumping", false);
        GetComponent<movement>().Inputvertical = 2;
        GetComponent<movement>().jumping = false;
    } 
}
