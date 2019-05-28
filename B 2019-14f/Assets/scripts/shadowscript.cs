using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shadowscript : MonoBehaviour {
	public Vector2 offset = new Vector2(-3, -3);
	private SpriteRenderer sprrndcaster;
	private SpriteRenderer sprrndshadow;
	private Transform transcaster;
	private Transform transshadow;
	public Material shadowmat;
	public Color shadowcolor;
	// Use this for initialization
	void Start () {
		transcaster = transform;
		transshadow = new GameObject().transform;
		transshadow.parent = transcaster;
		transshadow.gameObject.name = "shadow";
		transshadow.localRotation = Quaternion.identity;
		transshadow.localScale = transcaster.localScale;
		sprrndcaster = GetComponent<SpriteRenderer>();
		sprrndshadow = transshadow.gameObject.AddComponent<SpriteRenderer>();
		sprrndshadow.sortingLayerName = sprrndcaster.sortingLayerName;
		sprrndshadow.sortingOrder = sprrndcaster.sortingOrder - 1;
		sprrndshadow.material  = shadowmat;
		sprrndshadow.color = shadowcolor;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		transshadow.position = new Vector2(transcaster.position.x + offset.x, transcaster.position.y + offset.y);
		sprrndshadow.sprite = sprrndcaster.sprite;
	}
}
