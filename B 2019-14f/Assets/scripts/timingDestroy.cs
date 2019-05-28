using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timingDestroy : MonoBehaviour {

public float time = 30;
	// Use this for initialization
	void Update () {
		Destroy(gameObject, time);
	}
}
