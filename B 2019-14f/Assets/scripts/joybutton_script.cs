using UnityEngine;
using UnityEngine.EventSystems;

public class joybutton_script : MonoBehaviour, IPointerUpHandler, IPointerDownHandler {
	[HideInInspector]
	protected bool pressed;

	public void OnPointerDown(PointerEventData eventData)
	{
		pressed = true;
	}
	
	public void OnPointerUp(PointerEventData eventData)
	{
		pressed = false;
	}
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
