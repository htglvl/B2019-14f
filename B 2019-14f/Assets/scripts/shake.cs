using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shake : MonoBehaviour {
	public Animator cam_anim;
	public void Cam_Shake() 
	{
		cam_anim.SetTrigger("shake");
	}

/*goi ham: private shake shake;
paste vao phan start
	shake = GameObjace.FindGameObjectWithTag("ScreenShake").GetComponent<shake>;
	khi muon rung thi :shake.Cam_Shake();
 */
}
