using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class medkitscript : MonoBehaviour {

public float howmanyheal, how_many_filter_heal, directionleft, chieudai, Col2EnemyFrc, collideDamg, collideCooldown;
public LayerMask whatIsItems;
public Transform itemChecker;
private Rigidbody2D m_Rigidbody2D;
public SpriteRenderer me, myArm;
private float Pri_collideCooldown;

	void Start()
    {
		m_Rigidbody2D = GetComponent<Rigidbody2D>();
        Physics2D.queriesStartInColliders = true;
		Pri_collideCooldown = collideCooldown;
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
	private void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.layer == LayerMask.NameToLayer("block") && Pri_collideCooldown <= 0f)
    	{
			Pri_collideCooldown = collideCooldown;
            m_Rigidbody2D.AddForce(new Vector2(0f, Col2EnemyFrc));
			GetComponent<DestroyMySelf>().health -= collideDamg;
			me.color = new Color(0, 0.7f, 1, 1);
			myArm.color = new Color(0, 0.7f, 1, 1);

        }else
		{
			Pri_collideCooldown -= Time.deltaTime;
			me.color = new Color(1, 1, 1, 1);
			myArm.color = new Color(1, 1, 1, 1);
			Debug.Log(Pri_collideCooldown);
		}
    }
}
