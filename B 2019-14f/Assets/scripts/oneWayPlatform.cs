using UnityEngine;

public class oneWayPlatform : MonoBehaviour {
	private PlatformEffector2D effector;
	public GameObject player;
	private float jooystick; 
	public bool alive = true;
	public float KhoangCachGiuaPlayerVaThang;
	// Use this for initialization
	void Start () {
		effector = GetComponent<PlatformEffector2D>();
		player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if (player != null)
		{
			
			if (Vector2.Distance(transform.position, player.transform.position) < KhoangCachGiuaPlayerVaThang)
			{
				jooystick = player.GetComponent<PlayerMoveByCC>().Inputvertical;	
				if (jooystick == -1 )
				{
					effector.rotationalOffset = 180f;
				}
				else
				{
					effector.rotationalOffset = 0f;
				}

			}
		}	
	}
	void OnDrawGizmosSelected() {
			Gizmos.color = Color.red;
			Gizmos.DrawWireSphere(transform.position, KhoangCachGiuaPlayerVaThang);
		}
}
