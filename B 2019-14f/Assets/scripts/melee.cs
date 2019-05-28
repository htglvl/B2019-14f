using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class melee : MonoBehaviour {
	private float Pri_timebtwattack;
	public float timebtwattack, attackrange, damage, enemystun;
	public Transform attackpos;
	public LayerMask whatisenemy;
	public Animator firearm;
	void Update () {
		if(Pri_timebtwattack <= 0)
		{
			if (CrossPlatformInputManager.GetButton("Fire2"))
			{
				firearm.SetTrigger("is melee");
				Collider2D[] enemyToDamg = Physics2D.OverlapCircleAll(attackpos.position, attackrange, whatisenemy);
				for (int i = 0; i < enemyToDamg.Length; i++)
				{
					enemyToDamg[i].GetComponent<DestroyMySelf>().health -= damage;
					if (enemyToDamg[i].gameObject.layer == LayerMask.NameToLayer("enemy"))
    				{
                    	enemyToDamg[i].GetComponent<enemymove>().daze = true;
                	}
				Pri_timebtwattack = timebtwattack;
				}	
			}	
		}else
		{
			Pri_timebtwattack -=Time.deltaTime;
		}
	}
	void OnDrawGizmosSelected() {
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(attackpos.position, attackrange);
	}
	public void meleeSound()
	{
		FindObjectOfType<audiomanager>().Play("knife sound");	
	}
}
