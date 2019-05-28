using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class medkitscript : MonoBehaviour {

public float howmanyheal, how_many_filter_heal, directionleft, chieudai;
public LayerMask whatIsItems;
public Transform itemChecker;

	void Start()
    {
        Physics2D.queriesStartInColliders = true;
    }
	void Update () 
	{
		RaycastHit2D items = Physics2D.Raycast(itemChecker.position, Vector2.right * directionleft, chieudai, whatIsItems);
		if (items.collider != null)
		{
			Debug.DrawLine(itemChecker.position, items.point, Color.red);
			if (items.collider.CompareTag("medkit") == true && CrossPlatformInputManager.GetButton("Fire2"))
			{
				GetComponent<DestroyMySelf>().health += howmanyheal;
			}
			if (items.collider.CompareTag("filter") == true && CrossPlatformInputManager.GetButton("Fire2"))
			{
				GetComponent<healthdisplayforplayer>().filter += how_many_filter_heal;
			}
		}
	//	Debug.DrawLine(itemChecker.position, itemChecker.position + transform.right * chieudai * directionleft, Color.white);
	}
}
