using UnityEngine;

public class bulletcontroller : MonoBehaviour 
{
	public float speed, minspread, maxspread, speedRaw;
	private Vector2 randondirection;
	private shake cameraShake;
	private float randomY;
	public LayerMask whatisdestructible;
	public int damage;
	public float AreaOfEffect;
	public bool destroyWhenTouchEverything = true, normal_bullet = true;
	Rigidbody2D rb;
	public GameObject boom, great_boom;
	void Start () 
	{
		rb = GetComponent<Rigidbody2D>();
		randomY = Random.Range (minspread, maxspread);
		randondirection = new Vector2 (speed, randomY);
		cameraShake = GameObject.FindGameObjectWithTag("ScreenShake").GetComponent<shake>();
	}
	void Update()
	{
		rb.velocity = randondirection;
	}
	void OnCollisionEnter2D() 
	{
		cameraShake.Cam_Shake();
		if (normal_bullet == true)
		{
			Instantiate(boom, transform.position, Quaternion.identity);
		}else
		{
			Instantiate(great_boom, transform.position, Quaternion.identity);
		}
		Destroy(gameObject, 0f);
		if (CompareTag("destroyable")||CompareTag("Player")){
			Collider2D[] objectToDamage = Physics2D.OverlapCircleAll(transform.position, AreaOfEffect, whatisdestructible);
			for (int i = 0; i < objectToDamage.Length; i++)
			{
				DestroyMySelf obtdmg = objectToDamage[i].GetComponent<DestroyMySelf>();
				if (obtdmg != null )
				{
					obtdmg.health -= damage;
				} 
				if (objectToDamage[i].gameObject.layer == LayerMask.NameToLayer("enemy"))
    			{
          			objectToDamage[i].GetComponent<enemymove>().daze = true;
    			}
						
			}
		}
		
	}
		void OnDrawGizmosSelected() {
			Gizmos.color = Color.red;
			Gizmos.DrawWireSphere(transform.position, AreaOfEffect);
		}
}
