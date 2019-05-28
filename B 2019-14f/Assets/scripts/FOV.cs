using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOV : MonoBehaviour {
	[SerializeField] float viewRadius = 5;
	[SerializeField] LayerMask obstaclemask, playermask ;
	[SerializeField] float viewAngle = 135;
	Collider2D[] playerInradius;
	public List<Transform> visibleplayer = new List<Transform>();


	void FixedUpdate () 
	{
		findvisibleplayer();
		Debug.DrawLine(transform.position, transform.position + transform.right * viewRadius, Color.yellow);
	}	

	void findvisibleplayer()
	{
		playerInradius = Physics2D.OverlapCircleAll(transform.position, viewRadius);
		visibleplayer.Clear();
		for (int i = 0; i < playerInradius.Length; i++)
		{
				Transform player = playerInradius[i].transform;
				Vector2 dirPlayer = new Vector2(player.position.x-transform.position.x, player.position.y-transform.position.y);
				if (Vector2.Angle(dirPlayer, transform.right) < viewAngle/2)
				{
						float distancePlayer = Vector2.Distance(transform.position, player.position);
						if (!Physics2D.Raycast(transform.position, dirPlayer, distancePlayer, obstaclemask))
						{
								visibleplayer.Add(player);
						}
				}
		}
	}
	public Vector2 dirfromangle (float angledeg, bool global)
	{
		if (!global)
		{
				angledeg +=transform.eulerAngles.z;
		}
		return new Vector2(Mathf.Sin(angledeg) * Mathf.Deg2Rad, Mathf.Cos(angledeg) * Mathf.Deg2Rad );
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	
}
