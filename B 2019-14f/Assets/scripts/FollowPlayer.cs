using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour {
	
	public Transform player;
	private Vector2 velocity;
	public Vector3 mincamerapos;
	public Vector3 maxcamerapos;
	public float smoothtimeX;
	public float smoothtimeY;
	public bool bound;
	void LateUpdate ()
	{
		if (player != null)
		{
			float posX = Mathf.SmoothDamp(transform.position.x, player.transform.position.x, ref velocity.x, smoothtimeX);
			float posY = Mathf.SmoothDamp(transform.position.y, player.transform.position.y, ref velocity.y, smoothtimeY);
			transform.position = new Vector3(posX , posY, transform.position.z);
			if (bound)
			{
			transform.position = 
			new Vector3(transform.position.x, 
						Mathf.Clamp(transform.position.y, mincamerapos.y, maxcamerapos.y),
						Mathf.Clamp(transform.position.z, mincamerapos.z, maxcamerapos.z));
			}	
		}
	}	
}
