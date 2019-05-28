using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameracontroller : MonoBehaviour {
	public Transform target;
	public Vector3 offset;
	public bool YMaxEneable = false;
	public float YMaxValue = 0;
	public bool YMinEneable = false;
	public float YMinValue = 0;
	public bool XMaxEneable = false;
	public float XMaxValue = 0;
	public bool XMinEneable = false;
	public float XMinValue = 0;
	public float smoothtime = 0.15f;
	Vector3 velocity = Vector3.zero;
	
	void FixedUpdate() {

		Vector3 newpos = target.position + offset;
		newpos.z = transform.position.z;
		transform.position = Vector3.SmoothDamp(transform.position, newpos, ref velocity, smoothtime);

		if (YMaxEneable && YMinEneable)
		{
			newpos.y = Mathf.Clamp(target.position.y, YMinValue, YMaxValue);
		} else if (YMinEneable)
			{
				newpos.y = Mathf.Clamp(target.position.y, YMinValue, target.position.y);
			} else if (YMaxEneable)
			{
				Mathf.Clamp(target.position.y, target.position.y, YMaxValue);
			} 

		if (XMaxEneable && XMinEneable)
		{
			newpos.x = Mathf.Clamp(target.position.x, XMinValue, XMaxValue);
		} else if (XMinEneable)
			{
				newpos.x = Mathf.Clamp(target.position.x, XMinValue, target.position.x);
			} else if (XMaxEneable)
			{
				Mathf.Clamp(target.position.x, target.position.x, XMaxValue);
			} 
	
	}	

}